using System;
using R.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.Repositories;
using R.Services.IServices;
using R.Services.Services;
using R.Api;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // به جای localhost از AnyIP استفاده کن
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.AllowAnyOrigin()
         .WithOrigins(
            "http://localhost:3001"
       , "http://127.0.0.1:3001"
       , "http://127.0.0.1:5173"
       , "http://localhost:5173"
       , "http://127.0.0.1:3000"
       , "http://localhost:3000"
       , "http://5.223.41.164:4000"
       , "http://5.223.41.164:4001"
       , "http://localhost:4000"
       , "http://localhost:4001")

              .AllowAnyHeader()  // مجاز کردن هدرهای عمومی
              .AllowAnyMethod(); // مجاز کردن متدهای عمومی (GET, POST, PUT, DELETE)
    });
});

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPublicService, PublicService>();

builder.Services.AddDbContext<RDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
var app = builder.Build();

//app.UseMiddleware<TokenValidationMiddleware>();

//if (app.Environment.IsDevelopment())
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();
//app.UseMiddleware<TokenValidationMiddleware>();
app.MapControllers();

app.Urls.Add("http://0.0.0.0:5000");

app.Run();
