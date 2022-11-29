using HallWebApi.model.contender;
using HallWebApi.model.database;
using HallWebApi.model.database.context;
using HallWebApi.model.friend;
using HallWebApi.model.hall;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
services.AddControllers();
services.AddSingleton<BaseDbContext, PostgresqlDbContext>();
services.AddSingleton<IDbController, DbController>();
services.AddSingleton<IContenderGenerator, DbContenderLoader>();
services.AddSingleton<IHall, Hall>();
services.AddSingleton<IFriend, Friend>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();