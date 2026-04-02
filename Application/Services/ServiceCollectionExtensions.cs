using Lab5.Application.Abstractions.Persistence;
using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Sessions;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Application.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        string systemPassword)
    {
        services.AddScoped<IAccountService, AccountService>();

        services.AddScoped<ISessionService>(sp =>
        {
            IPersistenceContext context = sp.GetRequiredService<IPersistenceContext>();
            return new SessionService(context, systemPassword);
        });

        return services;
    }
}