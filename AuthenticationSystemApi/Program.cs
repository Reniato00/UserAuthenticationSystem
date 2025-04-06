using AuthenticationSystemApi.Extensions;
using AuthenticationSystemApi.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddFactories();
builder.Services.AddCustomFilters();

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<AuthorizationSettings>(builder.Configuration.GetSection(nameof(AuthorizationSettings.JwtSettings)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddCustomMiddlewares();

app.UseAuthorization();

app.MapControllers();

app.Run();
