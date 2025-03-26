using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoCall.Core.Entities;

namespace VideoCall.Pesistance.Persistance;


public class AppDbIdentityContext (DbContextOptions<AppDbIdentityContext> options) : IdentityDbContext<User>(options)
{
}
