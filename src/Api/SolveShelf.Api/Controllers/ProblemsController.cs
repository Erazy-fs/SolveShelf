using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolveShelf.Infrastructure.Persistence;

namespace SolveShelf.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProblemsController(SolveShelfDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var problems = await db.Problems
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync(ct);

        return Ok(problems);
    }
}