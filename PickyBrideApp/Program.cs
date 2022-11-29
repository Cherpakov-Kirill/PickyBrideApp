using Microsoft.Extensions.Hosting;
using PickyBride.contender;
using PickyBride.friend;
using PickyBride.hall;
using PickyBride.princess;
using Microsoft.Extensions.DependencyInjection;
using PickyBride.database;
using PickyBride.database.context;

namespace PickyBride;

public static class Program
{
    public const int MaxNumberOfContenders = 100;
    public const int NumberOfAttempts = 100;

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
                services.AddScoped<BaseDbContext, PostgresqlDbContext>();
                services.AddScoped<IDbController, DbController>();
                services.AddScoped<IContenderGenerator, DbContenderLoader>();
                services.AddScoped<IHall, Hall>();
                services.AddScoped<IFriend, Friend>();
            });
    }
}