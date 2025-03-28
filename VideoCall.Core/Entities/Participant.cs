using System.ComponentModel.DataAnnotations;
using VideoCall.Core.Dto;

namespace VideoCall.Core.Entities;

public class Participant
{
    [Key]
    public required string Id { get; set; }

    public required string Name { get; set; }
    public required string Role { get; set; }

    public DateTime JoinTime { get; set; }
    public DateTime LeaveTime { get; set; }

    public string? SessionId { get; set; }
    public Session? Session { get; set; }

    public string? User_Id { get; set; }
    public User? User { get; set; }

    public ParticipantDto ToParticipantDto()
    {
        return new ParticipantDto
        {
            id = Id,
            name = Name,
            role = Role,
        };
    }
}
