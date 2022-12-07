using HallWebApi.model.contender;
using HallWebApi.model.database;
using HallWebApi.model.database.context;
using HallWebApi.model.friend;
using HallWebApi.model.hall;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddControllers();
services.AddSingleton<BaseDbContext, PostgresqlDbContext>();
services.AddSingleton<IDbController, DbController>();
services.AddSingleton<IContenderGenerator, DbContenderLoader>();
services.AddSingleton<IHall, Hall>();
services.AddSingleton<IFriend, Friend>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
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