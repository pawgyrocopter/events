using Microsoft.AspNetCore.Mvc;

namespace EventApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    protected readonly ILogger _logger;

    public BaseController(ILogger logger)
    {
        _logger = logger;
    }
}