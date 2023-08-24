using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EmpCore.Persistence.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationDbContext(string connectionString)
    {
        if (String.IsNullOrWhiteSpace(connectionString))
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            throw new ArgumentException("Cannot be empty.", nameof(connectionString));
        }

        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}