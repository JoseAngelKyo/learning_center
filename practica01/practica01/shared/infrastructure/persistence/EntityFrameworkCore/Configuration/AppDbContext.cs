using Microsoft.EntityFrameworkCore;
using practica01.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using practica01.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;

namespace practica01.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>Provides the database context for the Hertz rental application.</summary>
/// <param name="options">The database context options.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        builder.UseSnakeCaseNamingConvention();
    }
}
