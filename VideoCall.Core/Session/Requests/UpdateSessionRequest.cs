namespace VideoCall.Core.Session.Requests;

public class UpdateSessionRequest
{
    public string? name { get; set; }
    public DateTime? startTime { get; set; }
    public DateTime? endTime { get; set; }
}
