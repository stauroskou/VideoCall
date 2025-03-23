namespace VideoCall.Core.Entities;

public class Participant
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
    public DateTime JoinTime { get; set; }
    public DateTime LeaveTime { get; set; }

    public Guid? SessionId { get; set; }
    public Session? Session { get; set; }
}
