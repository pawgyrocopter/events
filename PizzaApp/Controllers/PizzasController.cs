using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;
using PizzaApp.Services;

namespace PizzaApp.Controllers;

[Authorize]
public class PizzasController : BaseApiController
{
    private readonly IPizzaService _pizzaService;


    public PizzasController(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet]
    public async Task<IEnumerable<PizzaDto>> GetPizzas()
    {
        return await _pizzaService.GetPizzas();
    }

    [HttpGet("{name}", Name = "GetPizza")]
    public async Task<ActionResult<PizzaDto>> GetPizza(string name)
    {
        return await _pizzaService.GetPizza(name);
    }

    [Authorize(Roles = "PizzaMaker")]
    [HttpPost("add-pizza")]
    public async Task<ActionResult<PizzaDto>> AddPizza(IFormFile file, [FromQuery] PizzaDto pizzaDto)
    {
        // var pizza = await _unitOfWork.PizzaRepository.AddPizza(file, pizzaDto);
        // await _unitOfWork.Complete();
        return await _pizzaService.AddPizza(file, pizzaDto);
    }

    [Authorize(Roles = "PizzaMaker")]
    [HttpPut]
    public async Task<ActionResult<PizzaDto>> UpdatePizza([FromBody] PizzaDto pizzaDto)
    {
        // var pizza = await _unitOfWork.PizzaRepository.UpdatePizza(pizzaDto);
        // await _unitOfWork.Complete();
        return await _pizzaService.UpdatePizza(pizzaDto);
    }

    [Authorize(Roles = "PizzaMaker")]
    [HttpPut("{pizzaId:int}/{state:int}")]
    public async Task<ActionResult<PizzaDto>> UpdateStateOfOrderedPizza([FromRoute] int pizzaId, [FromRoute] int state)
    {
        return await _pizzaService.UpdatePizzaOrderState(pizzaId, state);
    }

    [HttpGet("orders/{orderId}")]
    public async Task<IEnumerable<PizzaDto>> GetPizzasByOrderId([FromRoute]int orderId)
    {
        return await _pizzaService.GetPizzasByOrderId(orderId);
        // return await _unitOfWork.PizzaRepository.GetPizzasByOrderId(orderId);
    }
}