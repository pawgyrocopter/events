using Domain.DTOs;
using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PizzaApp.Controllers;

[Authorize]
public class TopingsController : BaseApiController
{
    private readonly ITopingService _topingService;


    public TopingsController(ITopingService topingService)
    {
        _topingService = topingService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TopingDto>>> GetTopings()
    {
        return (await _topingService.GetTopings()).ToList();
    }

    [Authorize(Roles = "PizzaMaker")]
    [HttpPost]
    public async Task<ActionResult<TopingDto>> CreateToping([FromBody] TopingDto topingDto)
    {
        return await _topingService.CreateToping(topingDto.Name);
    }
}