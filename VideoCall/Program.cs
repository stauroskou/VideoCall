using Microsoft.EntityFrameworkCore;
using VideoCall.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


/*TODO: 
    Add a new controller to handle the Session and Participant entities
    Add a new service to handle the Session and Participant entities
    Add a new repository to handle the Session and Participant entities
    Add a new DTO to handle the Session and Participant entities
    Add a new mapping profile to handle the Session and Participant entities
    Add a new endpoint to handle the Session and Participant entities
    Add Authentication to the application with EntityFrameworkCore
    Add websockets support with SignalR
    Add a new service to handle the SignalR hub
    Add a new endpoint to handle the SignalR hub
    Add a new DTO to handle the SignalR hub
    Add MediatR to the application
    Add Logging to the application
*/