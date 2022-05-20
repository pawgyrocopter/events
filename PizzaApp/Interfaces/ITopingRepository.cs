using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs;

namespace PizzaApp.Interfaces;

public interface ITopingRepository
{
    Task<ActionResult<IEnumerable<TopingDto>>> GetTopings();
}