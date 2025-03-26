using System.ComponentModel.DataAnnotations;

namespace VideoCall.Core.Entities;

public class Session
{
    [Key]
    public required string Id { get; set; }

    public required string Name { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    public bool HasEndedByHost { get; set; }


    public bool HasEnded => HasEndedByHost || EndTime < DateTime.Now;
    public double Duration => (EndTime - StartTime).Hours;
    public int TotalParticipant => Participants.Count;

    public List<Participant> Participants { get; set; } = new();
}
