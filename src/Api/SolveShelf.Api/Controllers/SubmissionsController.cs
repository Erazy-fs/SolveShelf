using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using SolveShelf.Api.Kafka;
using SolveShelf.Contracts.Api;
using SolveShelf.Contracts.Messages;
using static System.Net.Mime.MediaTypeNames;

namespace SolveShelf.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class SubmissionsController(ISubmissionQueueProducer submissionQueueProducer, IResultsStore results) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateSubmissionResponse>> CreateSubmission([FromBody] CreateSubmissionRequest request)
    {
        var runId = Guid.NewGuid().ToString();

        Console.WriteLine($"New submission. Code length = {request.Code.Length}, RunId = {runId}");
        await submissionQueueProducer.EnqueueAsync(new SubmissionRequested()
        {
            RunId = runId,
            Code = request.Code,
            Tests = request.Tests,
        }, HttpContext.RequestAborted);

        return Ok(new CreateSubmissionResponse { RunId = runId });
    }

    [HttpGet("{runId}/result")]
    public IActionResult GetResult(string runId)
    {
        var result = results.Get(runId);

        if (result == null)
            return Ok(new SubmissionStatusResponse { Status = RequestStatus.Pending });

        return Ok(new SubmissionStatusResponse
        {
            Status = RequestStatus.Done,
            Success = result.Success,
            Output = result.Output
        });
    }
}