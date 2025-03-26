using Microsoft.EntityFrameworkCore;
using VideoCall.Core.Entities;

namespace VideoCall.Pesistance.Persistance;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Participant> Participants { get; set; }
}
