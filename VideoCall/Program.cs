using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using VideoCall.Core.Entities;
using VideoCall.Infrastructure.Configuration;
using VideoCall.Pesistance.Persistance;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultConfiguration();
builder.Services.AddMediatRConfiguration();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<AppDbIdentityContext>(
    options => options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = false;
});



builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbIdentityContext>()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();


/*TODO: 
    Ftiakse to User
    Add a new DTO to handle the Session and Participant entities
    Add websockets support with SignalR
    Add a new service to handle the SignalR hub
    Add a new endpoint to handle the SignalR hub
    Add a new DTO to handle the SignalR hub
    Add Logging to the application
*/