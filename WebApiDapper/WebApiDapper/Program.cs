using Serilog;
using Serilog.Formatting.Json;
using WebApiDapper.ActionFilters;
using WebApiDapper.DbContext;
using WebApiDapper.Entities;
using WebApiDapper.ExceptionFilters;
using WebApiDapper.IRepositories;
using WebApiDapper.IRepositories.Impl;
using WebApiDapper.Services;

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
    // Add Repository
    builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
    builder.Services.AddScoped(typeof(IProductRepository<>), typeof(ProductRepository<>));
    builder.Services.AddScoped(typeof(IExtendAttributeRepository<>), typeof(ExtendAttributeRepository<>));
    builder.Services.AddScoped(typeof(IAttributeValueNVarcharRepository<>), typeof(AttributeValueNVarcharRepository<>));
    builder.Services.AddScoped(typeof(IAttributeValueTextRepository<>), typeof(AttributeValueTextRepository<>));
    builder.Services.AddScoped(typeof(IAttributeValueIntRepository<>), typeof(AttributeValueIntRepository<>));
    builder.Services.AddScoped(typeof(IAttributeValueDecimalRepository<>), typeof(AttributeValueDecimalRepository<>));
    builder.Services.AddScoped(typeof(IAttributeValueDateTimeRepository<>), typeof(AttributeValueDateTimeRepository<>));
    // Add service
    builder.Services.AddScoped<ProductService>();
    builder.Services.AddScoped(typeof(ExtendAttributeService<>));

    //
    builder.Services.AddScoped<ValidationFilterAttribute>();
    builder.Services.AddScoped(typeof(ValidationIsExistEntity<>));
    builder.Services.AddScoped(typeof(ValidationNotExistEntityAttribute<,>));
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
