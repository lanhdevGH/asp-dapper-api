using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDapper.ActionFilters;
using WebApiDapper.DbContext;
using WebApiDapper.Entities;
using WebApiDapper.ExceptionFilters;
using WebApiDapper.IRepositories;
using WebApiDapper.IRepositories.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
builder.Services.AddSingleton<DapperDBContext>();
// Add Repo
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<ValidationNotExistEntityAttribute<Product>>();
builder.Services.AddScoped<ExceptionHandleFilter>();

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
