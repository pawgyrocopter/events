using Microsoft.AspNetCore.Mvc;

namespace PizzaApp.Interfaces;

public interface IAdminService
{
    Task<object> GetUsersWithRoles();
    Task<object> EditRoles(string userName, string roles);
}