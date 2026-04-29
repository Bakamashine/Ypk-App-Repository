using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    
    /// <summary>
    /// Служит только для миграции в sqlite
    /// Для миграций в основную БД, нужно добавить флаг --environment=Production
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlite(Environment.GetEnvironmentVariable("DbConnection"), b => { b.MigrationsAssembly("Persistence"); });
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}