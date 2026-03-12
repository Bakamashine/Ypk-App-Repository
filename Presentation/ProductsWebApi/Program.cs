using Aplication;
using Aplication.Interfaces;
using Application.Common.Mappings;
using CourseWebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using ProductsWebApi.Services;
using System.Reflection;
using System.Text;

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

    services.AddHttpContextAccessor();
    services.AddApplication();
    services.AddPersistance(builder.Configuration);
    services.AddControllers();

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
        app.UseSwaggerUI();
    }
    app.UseCustomExceptionHandler();
    app.UseRouting();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseStaticFiles();
    app.UseEndpoints(endpoints =>
    {
        app?.MapControllers();
    });
}
