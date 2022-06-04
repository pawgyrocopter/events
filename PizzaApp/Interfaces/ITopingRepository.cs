using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs;
using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface ITopingRepository
{
    Task<ActionResult<IEnumerable<TopingDto>>> GetTopings();
    Task<ActionResult<TopingDto>> CreateToping(string name);
}