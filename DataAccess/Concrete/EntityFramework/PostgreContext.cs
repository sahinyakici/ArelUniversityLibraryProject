using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework;

public class PostgreContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    public PostgreContext()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        this._configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string defaultConnection = _configuration["ConnectionStrings:DefaultConnection"];
        if (defaultConnection != null)
        {
            optionsBuilder.UseNpgsql(defaultConnection);
        }
    }
}