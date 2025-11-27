using Microsoft.AspNetCore.Mvc;

namespace SolveShelf.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolutionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetSolutions()
    {
        // временно — заглушка
        return Ok(new[] { "solution1", "solution2" });
    }
}