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


var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
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
