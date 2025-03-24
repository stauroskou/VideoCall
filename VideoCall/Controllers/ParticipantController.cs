using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoCall.Core.Entities;
using VideoCall.Infrastructure.Persistance;

namespace VideoCall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController(AppDbContext _appDbContext) : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateParticipant(Participant participant)
        {
            _appDbContext.Participants.Add(participant);
            _appDbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetParticipant(int sessionId)
        {
            var session = _appDbContext.Sessions.AsNoTracking()
                                                .FirstOrDefault(s => s.Id == sessionId);

            if(session != null)
            {
                var participants = session.Participants.ToList();
                return Ok(participants);
            }
            return BadRequest();
        }

        [HttpGet("AddToSession/{id}")]
        public async Task<IActionResult> AddParticipantToSession(int id, int sessionId)
        {
            var session = await _appDbContext.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
            if (session == null)
                return NotFound("Session not found");

            var participant = await _appDbContext.Participants.FirstOrDefaultAsync(s => s.Id == id);
            if (participant == null)
                return NotFound("Participant not found");

            participant.SessionId = sessionId;
            session.Participants.Add(participant);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("RemoveFromSession/{id}")]
        public async Task<IActionResult> RemoveParticipantFromSession(int id, int sessionId)
        {
            var session = await _appDbContext.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId);
            if (session == null)
                return NotFound("Session not found");

            var participant = session.Participants.FirstOrDefault(s => s.Id == id);
            if (participant == null)
                return NotFound("Participant is not in the session");

            participant.SessionId = null;
            session.Participants.Remove(participant);
            await _appDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
