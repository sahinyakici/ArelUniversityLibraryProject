using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorName = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookName = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    PageSize = table.Column<int>(type: "integer", nullable: false),
                    RentStatus = table.Column<bool>(type: "boolean", nullable: false),
                    Money = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreName = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    OperationClaimId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.OperationClaimId);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RentalStop = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalPrice = table.Column<float>(type: "real", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.RentalId);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    UserOperationClaimId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OperationClaimId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.UserOperationClaimId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "DeleteTime", "GenreName", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("0033d33a-14e0-405d-98cb-0845869b4cd4"), null, "Eğitim", false },
                    { new Guid("0d575337-7693-4525-b837-c388294c95e2"), null, "Spor", false },
                    { new Guid("16f76991-4e7c-4553-9a48-c7314929816b"), null, "Araştırma-İnceleme", false },
                    { new Guid("2b6abd6f-9566-4350-8a2c-4252337f179c"), null, "Edebiyat", false },
                    { new Guid("2c8f0e29-fc1c-4e5e-b554-685cf8d7a4d3"), null, "Söylev", false },
                    { new Guid("3453973f-468e-4553-b125-71edba802bd9"), null, "Bilim-Kurgu", false },
                    { new Guid("3764f862-335d-4ae6-81e8-156b23f4466f"), null, "Ekonomi-İş Dünyası", false },
                    { new Guid("3be6eff2-c95e-4019-a377-ebef4fda5b50"), null, "Manga", false },
                    { new Guid("43db7a16-180f-4b08-b0a7-c78e217ae71c"), null, "Korku-Gerilim", false },
                    { new Guid("48c4fc83-7ec8-4a23-9355-d7bad7afd24d"), null, "Roman", false },
                    { new Guid("5ae9edb2-827e-45e0-b045-ed26d73ed81f"), null, "Kişisel Gelişim", false },
                    { new Guid("5aeae4ad-ddb6-4388-a564-2ece1df2c4a6"), null, "Gezi", false },
                    { new Guid("5b589ba4-34c4-4f4e-8ff7-70ce1a3e3b3f"), null, "Psikoloji", false },
                    { new Guid("61b39f62-1b5e-464c-b893-85b19fa85cf2"), null, "Antropoloji-Etnoloji", false },
                    { new Guid("7d09a7aa-0d78-4d19-b2ca-c4a6e8b9bf65"), null, "Aksiyon", false },
                    { new Guid("85f647c6-b054-4d8e-8cfa-a6a8caeb08d7"), null, "Tarihj", false },
                    { new Guid("8d0e92d9-dfba-4403-b514-e31f2434d67d"), null, "Dünya Klasikleri", false },
                    { new Guid("8fc820ba-e985-4070-abe9-75b130c19d84"), null, "İnsan ve Toplum", false },
                    { new Guid("8fcccf91-bf68-4e65-a573-b520a033c902"), null, "Dini", false },
                    { new Guid("90025eb2-0499-4d4b-adf3-1b5d5119bfa8"), null, "Tiyatro", false },
                    { new Guid("94dc6f3f-9118-46ea-9a25-1207b6e75d0b"), null, "Hukuk", false },
                    { new Guid("94f73933-c7ef-4447-8042-a8a7644cca98"), null, "Biyografi", false },
                    { new Guid("966e07ee-ad95-4811-ac81-618e0adfa1e1"), null, "Sosyoloji", false },
                    { new Guid("97457d3a-6474-4612-acc5-65b46547dd22"), null, "Hikaye (Öykü)", false },
                    { new Guid("9fa69cf5-3718-4916-af06-892edaa16d35"), null, "Felsefe-Düşünce", false },
                    { new Guid("a654c1e5-e124-4116-ba7a-e98cea45cc3b"), null, "Anlatı", false },
                    { new Guid("a93f7ac6-731e-4411-933a-1a25871f8ccd"), null, "Macera-Aksiyon", false },
                    { new Guid("b74795fb-4ae2-474c-97f9-f1799109a7ac"), null, "Aşk", false },
                    { new Guid("c57961a1-8ee4-4531-b69f-5c055401b8b0"), null, "Sağlık-Tıp", false },
                    { new Guid("d1b83563-2e9c-45f6-940d-0b270075f3ec"), null, "Eğlence-Mizah", false },
                    { new Guid("e04bdb83-790a-4f82-a050-f811f0d1e683"), null, "Masal", false },
                    { new Guid("e427670a-5c82-4306-9b16-ec95d5edd9bf"), null, "Şiir", false },
                    { new Guid("f344800a-5686-49e3-ae93-2d0d621fd7c6"), null, "Çizgi-Roman", false },
                    { new Guid("fc1fbbbb-c63a-45a7-826c-78910849f1fc"), null, "Sanat", false }
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "OperationClaimId", "Name" },
                values: new object[,]
                {
                    { new Guid("11e2f8be-03a3-437d-ba2d-c1488c637bd1"), "user" },
                    { new Guid("16330cdc-380a-422b-918f-ff8e1c6300aa"), "genres.edit" },
                    { new Guid("22478ec9-32ff-4c0d-b044-52deedf7f704"), "authors.delete" },
                    { new Guid("3afeba83-b9c0-4ecf-8388-b70fbf30d55b"), "genres.delete" },
                    { new Guid("411b1c3b-2423-42e0-960f-1b8736f1ce19"), "books.update" },
                    { new Guid("6b900665-7b6d-4580-b9e0-378d78abbb2a"), "books.delete" },
                    { new Guid("746dbd86-8904-4e9f-9823-16e245b64c86"), "books.add" },
                    { new Guid("7b741a82-91d0-4b14-a4d0-39ed004ee683"), "admin" },
                    { new Guid("8a0c7825-3500-4871-9b62-3f7fb6d3f60f"), "authors.edit" },
                    { new Guid("99915bf4-ea32-4b11-9b20-aed6d5e01013"), "genres.update" },
                    { new Guid("ae3a7d84-6987-4d50-94c5-4be69c019200"), "genres.add" },
                    { new Guid("bfa4144b-161b-45df-9e5e-986b1ab4d59b"), "authors.add" },
                    { new Guid("c8fc5563-24ab-4ac2-b416-23d7dab7fa2b"), "authors.update" },
                    { new Guid("d1a05c36-dbf1-4742-8efd-82400debd807"), "books.edit" },
                    { new Guid("e252a4c7-c155-4211-a058-0f26ab9694ea"), "editor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
