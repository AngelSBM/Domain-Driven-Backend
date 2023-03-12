using DDD.Application.Services.Abstractions;
using AutoMapper;
using DDD.Application.Services.Root;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;
using DDD.Infrastructure;
using DDD.Infrastructure.Implementations;
using DDD.Infrastructure.Repositories;
using DDD.Utilities.AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionDB = builder.Configuration.GetConnectionString("DbServer");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionDB));
builder.Services.AddAutoMapper(typeof(ContactProfile).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IRepository<Address>, Repository<Address>>();

builder.Services.AddScoped<IContactService, ContactService>();


builder.Services.AddControllers();
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
