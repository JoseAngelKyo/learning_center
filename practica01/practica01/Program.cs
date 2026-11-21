using Microsoft.EntityFrameworkCore;
using practica01.Services.Application.CommandServices;
using practica01.Services.Application.Internal.CommandServices;
using practica01.Services.Application.Acl;
using practica01.Services.Domain.Repositories;
using practica01.Services.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using practica01.Services.Interfaces.Acl;
using practica01.Shared.Domain.Repositories;
using practica01.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using practica01.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using practica01.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using practica01.Shared.Infrastructure.Pipeline.Middleware.Components;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddOpenApi();
builder.Services.AddLocalization();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<practica01.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (!string.IsNullOrWhiteSpace(connectionString)) options.UseMySQL(connectionString);
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRentalOrderRepository, RentalOrderRepository>();
builder.Services.AddScoped<IRentalOrderCommandService, RentalOrderCommandService>();
builder.Services.AddScoped<IServicesContextFacade, ServicesContextFacade>();
var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();


