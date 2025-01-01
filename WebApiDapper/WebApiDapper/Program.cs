using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Formatting.Json;
using System.Text;
using WebAPICoreDapper.Data;
using WebAPICoreDapper.Models;
using WebApiDapper.DbContext;
using WebApiDapper.DTOs.ExtendAttribute;
using WebApiDapper.ExceptionFilters;
using WebApiDapper.Filter.ActionFilters;
using WebApiDapper.Filter.Auth;
using WebApiDapper.IRepositories;
using WebApiDapper.IRepositories.Impl;
using WebApiDapper.ProfileMapper;
using WebApiDapper.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new JsonFormatter())
    .CreateLogger();

try
{
    Log.Information("Starting web host. This is object: {0}", new { name = "thanh lanh" });
    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    // Authentication
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Định nghĩa trong appsettings.json
            ValidAudience = builder.Configuration["Jwt:Audience"], // Định nghĩa trong appsettings.json
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Key bí mật
        };

        // Đảm bảo mọi claim từ JWT đều được ánh xạ
        options.MapInboundClaims = false;
    });
    builder.Services.AddTransient<IClaimsTransformation, CustomClaimsTransformer>();
    // Swagger
    builder.Services.AddSwaggerGen(options =>
    {
        // Cấu hình để hỗ trợ Bearer Authentication
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Nhập 'Bearer' [khoảng trắng] và token của bạn trong ô bên dưới.\n\nVí dụ: Bearer abc123"
        });

        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });
    //
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddSingleton<DapperDBContext>();
    // Add Identity
    builder.Services.AddTransient<IUserStore<AppUser>, UserStore>();
    builder.Services.AddTransient<IRoleStore<AppRole>, RoleStore>();
    builder.Services.AddIdentity<AppUser, AppRole>().AddDefaultTokenProviders();
    builder.Services.Configure<IdentityOptions>(options =>
    {
        // Default Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    });
    // Add Repository
    builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
    builder.Services.AddScoped(typeof(IProductRepository<>), typeof(ProductRepository<>));
    builder.Services.AddScoped(typeof(IFunctionRepository<>), typeof(FunctionRepository<>));
    builder.Services.AddScoped(typeof(ICategoryRepository<>), typeof(CategoryRepository<>));
    builder.Services.AddScoped(typeof(IFunctionRepository<>), typeof(FunctionRepository<>));
    builder.Services.AddScoped(typeof(IExtendAttributeRepository<>), typeof(ExtendAttributeRepository<>));
    builder.Services.AddScoped(typeof(IAttributeValueNVarcharRepository<>), typeof(AttributeValueNVarcharRepository<>));
    builder.Services.AddScoped(typeof(IAttributeValueTextRepository<>), typeof(AttributeValueTextRepository<>));
    builder.Services.AddScoped(typeof(IAttributeValueIntRepository<>), typeof(AttributeValueIntRepository<>));
    builder.Services.AddScoped(typeof(IAttributeValueDecimalRepository<>), typeof(AttributeValueDecimalRepository<>));
    builder.Services.AddScoped(typeof(IAttributeValueDateTimeRepository<>), typeof(AttributeValueDateTimeRepository<>));
    // Add service
    builder.Services.AddScoped<RoleService>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<FunctionService>();
    builder.Services.AddScoped<PermissionService>();
    builder.Services.AddScoped<ProductService>();
    builder.Services.AddScoped<CategoryService>();
    builder.Services.AddScoped<ExtendAttributeService>();
    // Add Validation
    builder.Services.AddScoped<ValidationFilterAttribute>();
    builder.Services.AddScoped(typeof(ValidationIsExistEntity<>));
    builder.Services.AddScoped(typeof(ValidationNotExistEntityAttribute<,>));
    builder.Services.AddScoped<ExceptionHandleFilter>();
    builder.Host.UseSerilog((ctx, lg) => lg.WriteTo.Console());

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(c =>
        {
            c.PreSerializeFilters.Add((document, request) =>
            {
                var paths = document.Paths.ToDictionary(item => item.Key.ToLowerInvariant(), item => item.Value);
                document.Paths.Clear();
                foreach (var pathItem in paths)
                {
                    document.Paths.Add(pathItem.Key, pathItem.Value);
                }
            });
        });
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API V1");
        });
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
