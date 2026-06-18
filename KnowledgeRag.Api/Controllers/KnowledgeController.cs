using KnowledgeRag.Api.Models;
using KnowledgeRag.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeRag.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KnowledgeController : ControllerBase
{
    private readonly AnswerService _service;

    public KnowledgeController(
        AnswerService service)
    {
        _service = service;
    }

    [HttpPost("ask")]
    public async Task<IActionResult>
        Ask(AskRequest request)
    {
        var result =
            await _service
                .AskAsync(request.Question);

        return Ok(result);
    }
}