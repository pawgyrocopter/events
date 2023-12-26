using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PizzaApp.Controllers;
[Authorize(Roles = "Admin")]
public class AdminController : BaseApiController
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
       
    }
    // [HttpGet]
    // public async Task<ActionResult> GetUsersWithRoles()
    // {
    //     return Ok(await _adminService.GetUsersWithRoles());
    // }

    [HttpPost("edit-roles/{userName}")]
    public async Task<ActionResult> EditRoles(string userName, [FromQuery] string roles)
    {
        return Ok(await _adminService.EditRoles(userName, roles));
    }
}