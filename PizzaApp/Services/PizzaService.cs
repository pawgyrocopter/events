using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;

namespace PizzaApp.Services;

public class PizzaService : IPizzaService
{
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;
    private readonly IUnitOfWork _unitOfWork;

    public PizzaService(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
    {
        _mapper = mapper;
        _photoService = photoService;
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<PizzaDto>> GetPizzas()
    {
        return (await _unitOfWork.PizzaRepository.GetPizzas())
            .ProjectTo<PizzaDto>(_mapper.ConfigurationProvider);
    }

    public async Task<ActionResult<PizzaDto>> GetPizza(string name)
    {
        PizzaDto pizza = _mapper.Map<PizzaDto>(await _unitOfWork.PizzaRepository.GetPizza(name));
        pizza.Topings = new List<TopingDto>();
        return pizza;
    }

    public async Task<PizzaDto> GetPizzaByName(string pizzaName)
    {
        return _mapper.Map<PizzaDto>(await _unitOfWork.PizzaRepository.GetPizzaByName(pizzaName));
    }

    public async Task<ActionResult<PizzaDto>> AddPizza(IFormFile file, PizzaDto pizzaDto)
    {
        var result = await _photoService.AddPhotoAsync(file);

        // if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo()
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };
        var pizza = new Pizza()
        {
            Name = pizzaDto.Name,
            Photo = photo,
            Cost = pizzaDto.Cost,
            Ingredients = pizzaDto.Ingredients,
            Weight = pizzaDto.Weight,
            State = State.Pending,
        };
        PizzaDto createdPizza = _mapper.Map<PizzaDto>(await _unitOfWork.PizzaRepository.AddPizza(pizza, photo));
        await _unitOfWork.Complete();
        return createdPizza ;
    }

    public async Task<ActionResult<PizzaDto>> UpdatePizza(PizzaDto pizzaDto)
    {
        var pizza = await _unitOfWork.PizzaRepository.GetPizzaByName(pizzaDto.Name);
        pizza.Cost = pizzaDto.Cost;
        pizza.Ingredients = pizzaDto.Ingredients;
        pizza.Weight = pizzaDto.Weight;

        await _unitOfWork.PizzaRepository.UpdatePizza(pizza);
        await _unitOfWork.Complete();
        return _mapper.Map<PizzaDto>(pizza);
    }

    public async Task<PizzaDto> UpdatePizzaOrderState(int pizzaId, int state)
    {
        var pizza = await _unitOfWork.PizzaRepository.GetPizzaOrderById(pizzaId);
        var order = await _unitOfWork.OrderRepository.GetOrderByPizzaId(pizza.Id);
        if (pizza == null) return null;
        pizza.State = state switch
        {
            0 => State.Pending,
            1 => State.InProgress,
            2 => State.Ready,
            _ => State.Canceled
        };
        bool check = pizza.Order.Pizzas.All(x => x.State == State.Ready);
        if (check)
        {
            pizza.Order.OrderState = OrderState.Ready;
        }

        _unitOfWork.Complete();
        return _mapper.Map<PizzaDto>(pizza);
    }

    public async Task<IEnumerable<PizzaDto>> GetPizzasByOrderId(int orderId)
    {
        return (await _unitOfWork.PizzaRepository.GetPizzasByOrderId(orderId)).ProjectTo<PizzaDto>(_mapper.ConfigurationProvider);
    }
}