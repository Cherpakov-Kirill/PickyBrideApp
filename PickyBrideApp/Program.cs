using HallWebApi.model.friend;
using HallWebApi.model.hall;
using Microsoft.Extensions.Hosting;
using PickyBride.princess;
using Microsoft.Extensions.DependencyInjection;

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
                services.AddHostedService<Princess>();
                services.AddScoped<IHall, PickyBride.hall.Hall>();
                services.AddScoped<IFriend, PickyBride.friend.Friend>();
            });
    }
}