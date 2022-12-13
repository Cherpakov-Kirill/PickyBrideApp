using HallWebApi.model.friend;
using HallWebApi.model.hall;
using MassTransit;
using Microsoft.Extensions.Hosting;
using PickyBride.princess;
using Microsoft.Extensions.DependencyInjection;
using PickyBride.api;
using PickyBride.consumer;

namespace PickyBride;

public static class Program
{
    public const int NumberOfAttempts = 100;
    public const int MaxNumberOfContenders = 100;
    public static void Main(string[] args)
    {
        CreateAttemptsGeneratorHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateAttemptsGeneratorHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<Princess>();
                services.AddHostedService<Princess>(p => p.GetRequiredService<Princess>());
                services.AddScoped<HttpController>();
                services.AddScoped<IHall, PickyBride.hall.Hall>();
                services.AddScoped<ContenderConsumer>();
                services.AddScoped<IFriend, PickyBride.friend.Friend>();
                services.AddMassTransit(x =>
                {
                    x.UsingRabbitMq((context,cfg) =>
                    {
                        cfg.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });
                        cfg.ConfigureEndpoints(context);
                    });
                    x.AddConsumer<ContenderConsumer>();
                });
            });
    }
}