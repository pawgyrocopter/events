using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Helpers;
using PizzaApp.Interfaces;

namespace PizzaApp.Controllers;

[Authorize]
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

    [Authorize(Roles = "PizzaMaker")]
    [HttpPost]
    public async Task<ActionResult<TopingDto>> CreateToping([FromBody]TopingDto topingDto)
    {
        var toping = await _unitOfWork.TopingRepository.CreateToping(topingDto.Name);
        await _unitOfWork.Complete();
        return toping;
    }
}