using Microsoft.Extensions.DependencyInjection;
using VideoCall.Application.Participant;
using VideoCall.Application.Session;
using VideoCall.Core.Interfaces;

namespace VideoCall.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        services.AddScoped<IParticipantService, ParticipantService>();
        services.AddScoped<ISessionService, SessionService>();

        return services;
    }
}
