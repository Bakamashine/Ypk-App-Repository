using Aplication;
using Aplication.Interfaces;
using Application.Common.Mappings;
using CourseWebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using ProductsWebApi.Services;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.Console()
    .WriteTo.File("Logs/CourseWebApi-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .CreateLogger();
try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
RegisterServices(builder.Services);

var app = builder.Build();
await Configure(app);

var uploadsPath = Path.Combine(app.Environment.WebRootPath, "uploads", "reviews");

app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IFileStorageService, LocalFileStorageService>();

    services.Configure<FormOptions>(options =>
    {
        options.ValueLengthLimit = 10 * 1024 * 1024; 
        options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
    });


    services.AddAutoMapper(options =>
    {
        options.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        options.AddProfile(new AssemblyMappingProfile(typeof(IProductsDbContext).Assembly));
    });

    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });

        services.AddHttpContextAccessor();
    services.AddApplication();
    services.AddPersistance(builder.Configuration);
    services.AddControllers();

    services.AddSwaggerGen(config =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        config.IncludeXmlComments(xmlPath);
    });

    services.AddAuthentication(cnf =>
    {
        cnf.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        cnf.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer("Bearer", options =>
        {
            options.Audience = "ProductWebApi";
            options.RequireHttpsMetadata = false;
    
    
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = true,
                ValidAudience = "ProductWebApi",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["SECRET_KEY"])),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
        });


    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddScoped<IJwtTokenServise, JwtTokenService>();
    services.AddScoped<IPasswordHasherServise, PasswordHasherService>();

}

async Task Configure(WebApplication build)
{
    await app.InitializeDatabaseAsync();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            config.RoutePrefix = string.Empty;
            config.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
        });
    }
    app.UseCustomExceptionHandler();
    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseStaticFiles();
    app.UseEndpoints(endpoints =>
    {
        app?.MapControllers();
    });
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

