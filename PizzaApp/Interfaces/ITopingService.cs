using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs;

namespace PizzaApp.Interfaces;

public interface ITopingService
{
    Task<ActionResult<IEnumerable<TopingDto>>> GetTopings();
    Task<TopingDto> CreateToping(string name);
    Task<TopingDto> GetTopingById(int topingId);
}