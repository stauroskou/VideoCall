using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoCall.Core.Entities;

public class Participant
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public required string Role { get; set; }

    public DateTime JoinTime { get; set; }
    public DateTime LeaveTime { get; set; }

    public int? SessionId { get; set; }
    public Session? Session { get; set; }
}
