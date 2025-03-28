using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using VideoCall.Api.Hubs;
using VideoCall.Api.Notifications;
using VideoCall.Core.Entities;
using VideoCall.Core.Interfaces;
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:44372/",
            ValidAudience = "http://localhost:4200/",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };

    });

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<AppDbIdentityContext>(
    options => options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DefaultConnection")!));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = false;
});



builder.Services.AddScoped<ISessionNotifier, SessionNotifier>();

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppDbIdentityContext>()
    .AddApiEndpoints();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowed(_ => true);
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();
app.MapHub<SessionHub>("/sessionHub");



app.Run();


/*TODO: 
    Add a new DTO to handle the Session and Participant entities
    Add websockets support with SignalR
    Add a new service to handle the SignalR hub
    Add a new endpoint to handle the SignalR hub
    Add a new DTO to handle the SignalR hub
    Add Logging to the application
*/