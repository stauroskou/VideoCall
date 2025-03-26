using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoCall.Application.Abstractions.ApiResponse;
using VideoCall.Application.Participant.Commands.AddParticipant;
using VideoCall.Application.Participant.Commands.RemoveParticipant;
using VideoCall.Application.Participant.Queries.GetAllParticipants;

namespace VideoCall.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ParticipantController(ISender sender) : ControllerBase
{

    [HttpPost("addToSession/{sessionId}")]
    public async Task<IActionResult> AddParticipantToSession(string sessionId, [FromBody] string userId)
    {
        var command = new AddParticipantCommand(sessionId, userId);
        var result = await sender.Send(command);

        if(result.IsFailure)
        {
            return BadRequest(new GenericResponse<Core.Entities.Participant>(result.Error));
        }


        return Ok(new GenericResponse<Core.Entities.Participant>(result.Value));
    }

    [HttpPost("removeFromSession/{id}/{sessionId}")]
    public async Task<IActionResult> RemoveParticipantFromSession(string id, string sessionId)
    {
        var command = new RemoveParticipantCommand(id, sessionId);
        var result = await sender.Send(command);

        return Ok();
    }

    [HttpGet("getAllParticipantsFromSession/{sessionId}")]
    public async Task<IActionResult> GetAllParticipantsFromSession(string sessionId)
    {
        var query = new GetAllParticipantsQuery(sessionId);
        var result = await sender.Send(query);

        return Ok();
    }
}
