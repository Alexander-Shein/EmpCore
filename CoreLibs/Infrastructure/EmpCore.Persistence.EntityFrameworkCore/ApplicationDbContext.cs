using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmpCore.Persistence.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;
    private readonly Assembly _mappersAssembly;

    public ApplicationDbContext(string connectionString, Assembly mappersAssembly)
    {
        if (String.IsNullOrWhiteSpace(connectionString))
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            throw new ArgumentException("Cannot be empty.", nameof(connectionString));
        }
        _connectionString = connectionString;
        _mappersAssembly = mappersAssembly ?? throw new ArgumentNullException(nameof(mappersAssembly));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(_mappersAssembly);

        base.OnModelCreating(modelBuilder);
    }
}