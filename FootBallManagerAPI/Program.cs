using FootBallManagerAPI.Entities;

using FootBallManagerAPI.Models;
using FootBallManagerAPI.Repositories;
using FootBallManagerAPI.Repository;
using FootBallManagerAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FootBallManagerV2Context>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("FootBallManagerV2")));



builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var secretKey = builder.Configuration["AppSettings:SecretKey"];
var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        //Auto generate token
        ValidateIssuer = false,
        ValidateAudience = false,

        //get token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
        ClockSkew = TimeSpan.Zero
    };
});

//builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

///Add repository
builder.Services.AddScoped<IThongTinTranDauRepository, ThongTinTranDauRepository>();
builder.Services.AddScoped<IUserRolesRepository, UserRolesRepository>();
builder.Services.AddScoped<IThongtinGiaiDauRepository, ThongTinGiaiDauRepository>();
builder.Services.AddScoped<IThamGiaRepository, ThamGiaRepository>();
builder.Services.AddScoped<ITeamOfLeagueRepository, TeamOfLeagueRepository>();
builder.Services.AddScoped<ITapLuyenRepository, TapLuyenRepository>();
builder.Services.AddScoped<ISupplierServiceRepository, SupplierServiceRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IRoundRepository, RoundRepository>();
builder.Services.AddScoped<IQuocTichRepository, QuocTichRepository>();
builder.Services.AddScoped<IOtpRepository, OtpRepository>();
builder.Services.AddScoped<IDoiBongSupplierRepository, DoiBongSupplierRepository>();
builder.Services.AddScoped<ILeagueSupplierRepository, LeagueSupplierRepository>();


///Add repository

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
