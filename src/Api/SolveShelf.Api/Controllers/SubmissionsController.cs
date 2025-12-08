using Microsoft.AspNetCore.Mvc;
using SolveShelf.Api.Kafka;

namespace SolveShelf.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController(ISubmissionQueueProducer submissionQueueProducer) : ControllerBase
{
    public class CreateSubmissionRequest
    {
        public string Code { get; set; } = "";
        public string Tests { get; set; } = "";
    }

    public class CreateSubmissionResponse
    {
        public string RunId { get; set; }
    }

    [HttpPost]
    public async Task<ActionResult<CreateSubmissionResponse>> CreateSubmission([FromBody] CreateSubmissionRequest request)
    {
        // пока просто генерим runId и возвращаем
        var runId = Guid.NewGuid().ToString();

        Console.WriteLine($"New submission. Code length = {request.Code.Length}, RunId = {runId}");
        await submissionQueueProducer.EnqueueAsync(runId, request.Code, request.Tests, HttpContext.RequestAborted);

        return Ok(new CreateSubmissionResponse
        {
            RunId = runId
        });
    }
}