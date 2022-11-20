using Microsoft.EntityFrameworkCore.Migrations;

namespace Vacation_Booker.Migrations
{
    public partial class addUserIdToStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vacations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UnitSizes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Resorts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Regions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Areas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UnitSizes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Resorts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Areas");
        }
    }
}
