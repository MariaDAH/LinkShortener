using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkShortener.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Insertusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Name", "Email", "CreatedDate" },
                values: new object[,]
                {
                    { "maria", "maria@online.io", DateTime.Now },
                    { "marc", "marc@online.io", DateTime.Now }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Url]");
            migrationBuilder.Sql("DELETE FROM [User]");
        }
    }
}
