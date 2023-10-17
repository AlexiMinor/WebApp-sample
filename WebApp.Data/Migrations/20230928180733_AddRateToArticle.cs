using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.MVC7.Migrations
{
    /// <inheritdoc />
    public partial class AddRateToArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rate",
                table: "Articles",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Articles");
        }
    }
}
