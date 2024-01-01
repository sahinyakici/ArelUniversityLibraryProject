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
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
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
                    { new Guid("03d5e96a-fa3e-4a64-b032-fe9aeff2fd6b"), null, "Şiir", false },
                    { new Guid("178da562-69ee-487b-af24-04095f59fe08"), null, "Eğitim", false },
                    { new Guid("1b3b7ec1-1948-40ff-92ea-cb14d34aa5f7"), null, "Söylev", false },
                    { new Guid("203b80f5-3d92-4fbf-b239-438ef3c1d910"), null, "Masal", false },
                    { new Guid("2640be2d-53ff-4b11-8cea-3a6e326fa1a0"), null, "Hikaye (Öykü)", false },
                    { new Guid("3044729a-4f11-4056-922e-2d5428a91816"), null, "Spor", false },
                    { new Guid("36ee2253-94ef-4589-93ad-2e643dce4af9"), null, "Tiyatro", false },
                    { new Guid("394f9039-8152-4657-9949-1111794fe2b1"), null, "Araştırma-İnceleme", false },
                    { new Guid("43c5d6dc-97c0-4844-8daa-db0235d47cae"), null, "Hukuk", false },
                    { new Guid("511bb5dd-e77d-41c0-a4b0-36f1b8df21fa"), null, "Psikoloji", false },
                    { new Guid("5767be66-cb6b-4848-89a9-eff3106650ef"), null, "Çizgi-Roman", false },
                    { new Guid("58b08019-24b0-4ff7-a14b-ab7078e1c803"), null, "Macera-Aksiyon", false },
                    { new Guid("600513fd-520e-4f04-a363-d329be47e7e9"), null, "Eğlence-Mizah", false },
                    { new Guid("64db2346-d19d-476d-8b46-a10bb20ff8ae"), null, "Antropoloji-Etnoloji", false },
                    { new Guid("6caeccd2-72f3-4dc8-ae52-a56088a7aa4a"), null, "Aksiyon", false },
                    { new Guid("6d6c6e0e-ce34-48f8-be00-ec917e7a02e3"), null, "Biyografi", false },
                    { new Guid("7302df79-76d1-41a1-9a00-6d2d0f7d5eb0"), null, "Sağlık-Tıp", false },
                    { new Guid("7a7c0082-92ac-422f-98db-d10a4bc91e16"), null, "Anlatı", false },
                    { new Guid("81f17f5d-801c-40af-8c69-403f1f24f021"), null, "Kişisel Gelişim", false },
                    { new Guid("82ea624a-ae0d-4be9-a308-9662ade88b57"), null, "Roman", false },
                    { new Guid("86a2d552-0086-493c-8aaf-ef4d5a12fa92"), null, "Tarihj", false },
                    { new Guid("8a4e374c-54f0-4468-91c0-724b950fb052"), null, "Gezi", false },
                    { new Guid("8c8ad5af-620e-43ed-8f97-fdb56bb08ef9"), null, "Sanat", false },
                    { new Guid("92e0b5f5-492d-4088-bdd6-7bbafbe2fc0d"), null, "Ekonomi-İş Dünyası", false },
                    { new Guid("b5a73b73-6bc7-4884-8885-acc4e4ad3bb2"), null, "Sosyoloji", false },
                    { new Guid("b6468550-8269-4667-b1e0-5a4537edd602"), null, "Felsefe-Düşünce", false },
                    { new Guid("bd5cd2df-8a9f-4f4e-9ad6-063c3055873d"), null, "İnsan ve Toplum", false },
                    { new Guid("be587145-e740-481f-84db-286a78b4826f"), null, "Korku-Gerilim", false },
                    { new Guid("c1a43b50-9f1d-4594-acc3-111b6b7db283"), null, "Aşk", false },
                    { new Guid("ccaf7735-89ec-4145-954a-a944d8937191"), null, "Bilim-Kurgu", false },
                    { new Guid("d3faf1f7-6cbf-4a0d-a252-424c2ad88410"), null, "Dini", false },
                    { new Guid("da489254-540c-4ea6-bdc7-aa2872969352"), null, "Dünya Klasikleri", false },
                    { new Guid("dea72350-0ca4-47bb-9202-553d8b39093d"), null, "Manga", false },
                    { new Guid("fa5d5865-b3c1-4987-a02e-ceb3092b92bd"), null, "Edebiyat", false }
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "OperationClaimId", "Name" },
                values: new object[,]
                {
                    { new Guid("1ddb1f78-9dd6-46c1-93ad-870cfe5b1101"), "authors.add" },
                    { new Guid("394a4242-e7a8-428c-9e40-d6c014d1e0b5"), "user" },
                    { new Guid("4630e128-a18b-4ac5-ac9f-98dad59dad69"), "books.update" },
                    { new Guid("5237d1db-c5c0-4f93-a20f-01c228c65541"), "genres.delete" },
                    { new Guid("53853f74-f441-4eea-b8a0-743d813085a2"), "admin" },
                    { new Guid("5a0f30b7-fe47-4c78-abbc-a24b5dedb9fb"), "genres.edit" },
                    { new Guid("983d2f56-bdd2-44a2-83e6-1e11dd13bb07"), "genres.add" },
                    { new Guid("a6d3fb56-4689-4c94-a0ac-640c635d4b22"), "genres.update" },
                    { new Guid("c1d4484b-84b2-4347-88bf-9c32398c9c1a"), "books.add" },
                    { new Guid("caa202c3-923f-4101-bc3f-c90b3d1a2ed6"), "books.edit" },
                    { new Guid("cc0b474b-a845-4d83-9950-c818997c41b7"), "authors.edit" },
                    { new Guid("cf62a764-d48d-451a-b3cc-8b3f1f6e543d"), "books.delete" },
                    { new Guid("e00dc8a9-b6b3-4735-918c-8a12c08c3e9f"), "authors.update" },
                    { new Guid("f90454f7-441f-4d6e-add3-5e3ea725aab3"), "authors.delete" },
                    { new Guid("fa53fd02-de0b-45d8-a064-9a1b24193665"), "editor" }
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
                name: "Images");

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
