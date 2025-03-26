using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoCall.Application.Abstractions.ApiResponse;
using VideoCall.Application.Session.Commands.CreateSession;
using VideoCall.Application.Session.Commands.DeleteSession;
using VideoCall.Application.Session.Commands.UpdateSession;
using VideoCall.Application.Session.Queries.GetSessions;
using VideoCall.Application.Session.Queries.GetSessionsById;
using VideoCall.Core.Entities;
using VideoCall.Core.Session.Requests;
using VideoCall.Pesistance.Persistance;

namespace VideoCall.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SessionController(AppDbContext _appContext, ISender sender) : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateSession([FromBody]CreateSessionRequest request)
    {
        var command = new CreateSessionCommand(request.name, request.startTime, request.endTime);
        var result = await sender.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(new GenericResponse<Session>(result.Error));
        }
        return Ok(new GenericResponse<Session>(result.Value));
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetSessions()
    {
        var query = new GetSessionsQuery();
        var sessions = await sender.Send(query);
        return Ok(new GenericResponse<List<Session>?>(sessions.Value));
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetSession(string id)
    {
        var query = new GetSessionByIdQuery(id);
        var session = await sender.Send(query);

        if (session.IsFailure)
        {
            return BadRequest(new GenericResponse<Session>(session.Error));
        }

        return Ok(new GenericResponse<Session>(session.Value));
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateSession(string id, [FromBody] UpdateSessionRequest request)
    {
        var command = new UpdateSessionCommand(id, request.name, request.startTime, request.endTime);
        var result = await sender.Send(command);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteSession(string id)
    {
        var command = new DeleteSessionCommand(id);
        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(new GenericResponse<Session>(result.Error));
        }

        return Ok(new GenericResponse<bool>(result.Value));
    }
}
