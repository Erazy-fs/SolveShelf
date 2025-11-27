using Microsoft.AspNetCore.Mvc;

namespace SolveShelf.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController : ControllerBase
{
    public class CreateSubmissionRequest
    {
        public string Code { get; set; } = "";
    }

    public class CreateSubmissionResponse
    {
        public Guid RunId { get; set; }
    }

    [HttpPost]
    public IActionResult CreateSubmission([FromBody] CreateSubmissionRequest request)
    {
        // пока просто генерим runId и возвращаем
        var runId = Guid.NewGuid();

        Console.WriteLine($"New submission. Code length = {request.Code.Length}, RunId = {runId}");

        return Ok(new CreateSubmissionResponse
        {
            RunId = runId
        });
    }
}