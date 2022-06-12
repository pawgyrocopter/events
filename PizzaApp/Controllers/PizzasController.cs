using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;
using PizzaApp.Services;

namespace PizzaApp.Controllers;

public class PizzasController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;


    public PizzasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IEnumerable<PizzaDto>> GetPizzas()
    {
        return await _unitOfWork.PizzaRepository.GetPizzas();
    }

    [HttpGet("{name}", Name = "GetPizza")]
    public async Task<ActionResult<PizzaDto>> GetPizza(string name)
    {
        return await _unitOfWork.PizzaRepository.GetPizza(name);
    }

    [HttpPost("add-pizza")]
    public async Task<ActionResult<Pizza>> AddPizza(IFormFile file, [FromQuery] PizzaDto pizzaDto)
    {
        var pizza = await _unitOfWork.PizzaRepository.AddPizza(file, pizzaDto);
        await _unitOfWork.Complete();
        return pizza;
    }

    [HttpPut]
    public async Task<ActionResult<PizzaDto>> UpdatePizza([FromBody] PizzaDto pizzaDto)
    {
        var pizza = await _unitOfWork.PizzaRepository.UpdatePizza(pizzaDto);
        await _unitOfWork.Complete();
        return pizza;
    }

    [HttpPut("{pizzaId:int}/{state:int}")]
    public async Task<ActionResult<PizzaDto>> UpdateStateOfOrderedPizza([FromRoute] int pizzaId, [FromRoute] int state)
    {
        var pizza = await _unitOfWork.PizzaRepository.UpdatePizzaOrderState(pizzaId, state);
        
        await _unitOfWork.Complete();
        return pizza;
    }

    [HttpGet("orders/{orderId}")]
    public async Task<IEnumerable<PizzaDto>> GetPizzasByOrderId([FromRoute]int orderId)
    {
        return await _unitOfWork.PizzaRepository.GetPizzasByOrderId(orderId);
    }
}