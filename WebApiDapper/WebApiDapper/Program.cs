using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Context;
using Serilog.Formatting.Json;
using WebApiDapper.ActionFilters;
using WebApiDapper.DbContext;
using WebApiDapper.Entities;
using WebApiDapper.ExceptionFilters;
using WebApiDapper.IRepositories;
using WebApiDapper.IRepositories.Impl;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new JsonFormatter())
    .CreateLogger();

try
{
    Log.Information("Starting web host. This is object: {0}", new {name = "thanh lanh"});
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
    builder.Host.UseSerilog((ctx, lg) => lg.WriteTo.Console());

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.ConfigureExceptionHandler();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
