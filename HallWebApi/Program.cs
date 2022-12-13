using HallWebApi.model.contender;
using HallWebApi.model.database;
using HallWebApi.model.database.context;
using HallWebApi.model.friend;
using HallWebApi.model.hall;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(System.Configuration.ConfigurationManager.AppSettings["RebbitMQHost"],
            System.Configuration.ConfigurationManager.AppSettings["RebbitMQVirtualHost"],
            h =>
            {
                h.Username(System.Configuration.ConfigurationManager.AppSettings["RebbitMQUsername"]);
                h.Password(System.Configuration.ConfigurationManager.AppSettings["RebbitMQPassword"]);
            }
        );
        cfg.ConfigureEndpoints(context);
    });
});
services.AddControllers();
services.AddSingleton<BaseDbContext, PostgresqlDbContext>();
services.AddSingleton<IDbController, DbController>();
services.AddSingleton<IContenderGenerator, DbContenderLoader>();
services.AddSingleton<IHall, Hall>();
services.AddSingleton<IFriend, Friend>();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HallWebApi - V1", Version = "v1" });

    var filePath = Path.Combine(AppContext.BaseDirectory, "HallWebApi.xml");
    c.IncludeXmlComments(filePath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.Run();