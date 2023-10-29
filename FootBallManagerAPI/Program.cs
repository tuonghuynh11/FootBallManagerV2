using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FootBallManagerV2Context>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("FootBallManagerV2")));

builder.Services.AddScoped<ICauthuRepository, CauthuRepository>();
builder.Services.AddScoped<IChuyennhuongRepository, ChuyennhuongRepository>();
builder.Services.AddScoped<IDiadiemRepository, DiadiemRepository>();
builder.Services.AddScoped<IDiemRepository, DiemRepository>();
builder.Services.AddScoped<IDoibongRepository, DoibongRepository>();
builder.Services.AddScoped<IDoihinhchinhRepository, DoihinhchinhRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IFootballmatchRepository, FootballmatchRepository>();
builder.Services.AddScoped<IHuanluyenvienRepository, HuanluyenvienRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemtypeRepository, ItemtypeRepository>();
builder.Services.AddScoped<ILeagueRepository, LeagueRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IFieldServiceRepository, FieldserviceRepository>();

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
