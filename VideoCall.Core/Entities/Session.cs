namespace VideoCall.Core.Entities;

public class Session
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double Duration => (EndTime - StartTime).Hours;

    public List<Participant> Participants { get; set; } = new();
}
