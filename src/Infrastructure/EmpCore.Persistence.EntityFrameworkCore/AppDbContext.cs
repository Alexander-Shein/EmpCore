using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmpCore.Persistence.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    private readonly string _connectionString;
    private readonly Assembly _persistenceAssembly;

    public AppDbContext()
    {
        _persistenceAssembly = Assembly.GetExecutingAssembly();
    }

    public AppDbContext(string connectionString, Assembly persistenceAssembly)
    {
        if (String.IsNullOrWhiteSpace(connectionString))
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            throw new ArgumentException("Cannot be empty.", nameof(connectionString));
        }
        _connectionString = connectionString;
        _persistenceAssembly = persistenceAssembly ?? throw new ArgumentNullException(nameof(persistenceAssembly));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString, 
            x => x.MigrationsAssembly(_persistenceAssembly.FullName));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_persistenceAssembly);
        
        base.OnModelCreating(modelBuilder);
    }

}