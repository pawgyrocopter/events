using AutoMapper;
using AutoMapper.QueryableExtensions;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;

namespace PizzaApp.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
    {
        var order = new Order();
        var users = await _unitOfWork.UserRepository.GetUsers();
        order.User = users.FirstOrDefault(x => x.UserName.Equals(orderDto.Name));
        var pizzas = new List<PizzaOrder>();
        foreach (var pizza in orderDto.Pizzas)
        {
            var pOrder = new PizzaOrder();
            pOrder.Pizza = await _unitOfWork.PizzaRepository.GetPizzaByName(pizza.Name);
            pOrder.Topings = pizza.Topings
                .Select(x => new TopingOrder()
                {
                    Toping =  _unitOfWork.TopingRepository.GetTopingById(x.Id).Result,
                    Counter = x.Counter
                }).ToList();
            pOrder.Pizza.Cost = pizza.Cost;
            pOrder.State = State.Pending;
            pizzas.Add(pOrder);
        }
        order.Pizzas = pizzas;

        OrderDto createdOrderDto = await _unitOfWork.OrderRepository.CreateOrder(order);
        await _unitOfWork.Complete();
        return createdOrderDto;
    }

    public async Task<bool> UpdateOrderState(int orderId)
    {
        var order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(orderId);
        var answer = order.Pizzas.All(x => x.State == State.Ready);
        if (answer) order.OrderState = OrderState.Ready;
        await _unitOfWork.Complete();
        return answer;
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
    {
        return (await _unitOfWork.OrderRepository.GetOrders())
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider);
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string name)
    {
        return (await _unitOfWork.OrderRepository.GerUserOrders(name))
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider);
    }
}