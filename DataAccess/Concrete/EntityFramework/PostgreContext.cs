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
    public DbSet<Image> Images { get; set; }
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OperationClaim[] operationClaims = new[]
        {
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "admin" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "user" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "editor" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "books.add" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "books.edit" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "books.delete" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "books.update" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "authors.add" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "authors.edit" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "authors.delete" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "authors.update" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "genres.add" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "genres.edit" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "genres.delete" },
            new OperationClaim { OperationClaimId = Guid.NewGuid(), Name = "genres.update" }
        };
        Genre[] genres = new[]
        {
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Dünya Klasikleri", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Aşk", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Roman", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Psikoloji", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Söylev", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Dini", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Tarihj", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Korku-Gerilim", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Aksiyon", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Kişisel Gelişim", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Şiir", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Macera-Aksiyon", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Felsefe-Düşünce", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Edebiyat", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Bilim-Kurgu", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Hikaye (Öykü)", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Sosyoloji", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Biyografi", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Araştırma-İnceleme", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Manga", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Ekonomi-İş Dünyası", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Masal", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Eğlence-Mizah", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Sağlık-Tıp", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "İnsan ve Toplum", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Çizgi-Roman", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Eğitim", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Tiyatro", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Hukuk", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Sanat", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Antropoloji-Etnoloji", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Spor", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Gezi", IsDeleted = false },
            new Genre { GenreId = Guid.NewGuid(), GenreName = "Anlatı", IsDeleted = false },
        };
        modelBuilder.Entity<OperationClaim>().HasData(operationClaims);
        modelBuilder.Entity<Genre>().HasData(genres);
    }
}