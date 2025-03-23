using Microsoft.EntityFrameworkCore;
using VideoCall.Core.Entities;

namespace VideoCall.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Session> Sessions { get; set; }
    public DbSet<Participant> Participants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>()
            .HasMany(s => s.Participants)
            .WithOne(p => p.Session)
            .HasForeignKey(p => p.SessionId);

        base.OnModelCreating(modelBuilder);
    }
}
