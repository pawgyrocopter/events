﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;

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

    
}