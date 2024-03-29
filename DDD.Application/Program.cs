using DDD.Application.Services.Abstractions;
using AutoMapper;
using DDD.Application.Services.Root;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;
using DDD.Infrastructure;
using DDD.Infrastructure.Implementations;
using DDD.Infrastructure.Repositories;
using DDD.Utilities.AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using DDD.Utilities.DTOs.Contact;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using DDD.Application.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(ErrorResponse));
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<NewContactDto>();

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

var corsConfig = "_corsConfig";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsConfig,
                      policy =>
                      {
                          policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

var app = builder.Build();


app.UseMiddleware<ExceptionHandler>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}
app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

app.UseCors(corsConfig);

app.UseAuthorization();

app.MapControllers();

app.Run();
