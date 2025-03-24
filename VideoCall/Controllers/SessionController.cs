using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoCall.Core.Entities;
using VideoCall.Infrastructure.Persistance;

namespace VideoCall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController(AppDbContext _appContext) : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateSession(Session session)
        {
            _appContext.Sessions.Add(session);
            _appContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetSessions()
        {
            var sessions = _appContext.Sessions.Include(s => s.Participants).ToList();
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public IActionResult GetSession(int id)
        {
            var session = _appContext.Sessions.FirstOrDefault(s => s.Id == id);
            return Ok(session);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSession(int id, Session session)
        {
            var existingSession = _appContext.Sessions.FirstOrDefault(s => s.Id == id);
            if (existingSession == null)
            {
                return NotFound();
            }
            existingSession.Name = session.Name;
            existingSession.StartTime = session.StartTime;
            existingSession.EndTime = session.EndTime;
            _appContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSession(int id)
        {
            var session = _appContext.Sessions.FirstOrDefault(s => s.Id == id);
            if (session == null)
            {
                return NotFound();
            }
            _appContext.Sessions.Remove(session);
            _appContext.SaveChanges();
            return Ok();
        }
    }
}
