using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs;
using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface ITopingRepository
{
    Task<IQueryable<Toping>> GetTopings();
    Task<Toping> CreateToping(string name);

    Task<Toping> GetTopingById(int topingId);
}