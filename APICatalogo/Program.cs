using APICatalogo.Configs;
using Infrastructure.Context;
using Infrastructure.Repository;
using Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options =>
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
         ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
     });

SwaggerConfig.ConfigureServices(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    SwaggerConfig.Configure(app);
}

app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
