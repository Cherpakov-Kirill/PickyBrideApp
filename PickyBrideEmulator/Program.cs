using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PickyBride.contender;
using PickyBride.database;
using PickyBride.database.context;
using PickyBride.friend;
using PickyBride.hall;
using PickyBride.princess;

namespace PickyBrideEmulator;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            throw new ApplicationException("Incorrect program arguments.\nShould be one arg: numberOfEmulatingAttempt");
        }
        CreateAttemptsEmulatorHostBuilder(args).Build().Run();
    }
    
    private static IHostBuilder CreateAttemptsEmulatorHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHostedService<Princess>(provider =>
                    new Princess(
                    provider.GetRequiredService<IHostApplicationLifetime>(),
                    provider.GetRequiredService<IHall>(),
                    provider.GetRequiredService<IFriend>(),
                    1,int.Parse(args[0])
                    )
                );
                services.AddScoped<BaseDbContext, PostgresqlDbContext>();
                services.AddScoped<IDbController, DbController>();
                services.AddScoped<IContenderGenerator, DbContenderLoader>();
                services.AddScoped<IHall, Hall>();
                services.AddScoped<IFriend, Friend>();
            });
    }
}