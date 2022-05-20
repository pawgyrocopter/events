using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Helpers;
using PizzaApp.Interfaces;

namespace PizzaApp.Controllers;

public class TopingsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;

    public TopingsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TopingDto>>> GetTopings()
    {
        return await _unitOfWork.TopingRepository.GetTopings();
    }
}