using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "TVShows",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Stars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Directors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "TVShows");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Stars");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "Directors");
        }
    }
}
