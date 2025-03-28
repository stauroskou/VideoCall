using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoCall.Application.Abstractions.ApiResponse;
using VideoCall.Application.Participant.Commands.AddParticipant;
using VideoCall.Application.Participant.Commands.RemoveParticipant;
using VideoCall.Application.Participant.Queries.GetAllParticipants;
using VideoCall.Core.Dto;
using VideoCall.Core.Entities;

namespace VideoCall.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ParticipantController(ISender sender) : ControllerBase
{

    [HttpPost("addToSession/{sessionId}/{userId}")]
    public async Task<IActionResult> AddParticipantToSession(string sessionId, string userId)
    {
        var command = new AddParticipantCommand(sessionId, userId);
        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(new GenericResponse<Participant>(result.Error));
        }

        return Ok(new GenericResponse<Participant?>(result.Value));
    }

    [HttpPost("removeFromSession/{id}/{sessionId}")]
    public async Task<IActionResult> RemoveParticipantFromSession(string id, string sessionId)
    {
        var command = new RemoveParticipantCommand(id, sessionId);
        var result = await sender.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(new GenericResponse<Participant>(result.Error));
        }

        return Ok(new GenericResponse<string>($"Removed participant: {result.Value}"));
    }

    [HttpGet("getAllParticipantsFromSession/{sessionId}")]
    public async Task<IActionResult> GetAllParticipantsFromSession(string sessionId)
    {
        var query = new GetAllParticipantsQuery(sessionId);
        var result = await sender.Send(query);

        if(result.IsFailure)
        {
            return BadRequest(new GenericResponse<List<ParticipantDto>>(result.Error));
        }

        List<ParticipantDto> participants = result.Value
            .Select(s => s.ToParticipantDto())
            .ToList();

        return Ok(new GenericResponse<List<ParticipantDto>>(participants));
    }
}
