using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.MVC7.Migrations
{
    /// <inheritdoc />
    public partial class AddRssUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RssUrl",
                table: "ArticleSources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RssUrl",
                table: "ArticleSources");
        }
    }
}
