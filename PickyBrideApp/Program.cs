using Microsoft.Extensions.Hosting;
using PickyBride.contender;
using PickyBride.friend;
using PickyBride.hall;
using PickyBride.princess;
using Microsoft.Extensions.DependencyInjection;

namespace PickyBride;

public static class Program
{
    public const int MaxNumberOfContenders = 100;

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHostedService<Princess>();
                services.AddScoped<IHall, Hall>();
                services.AddScoped<IFriend, Friend>();
                services.AddScoped<IContenderGenerator, ContenderGenerator>();
            });
    }
}