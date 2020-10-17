using Microsoft.EntityFrameworkCore.Migrations;

namespace Vacation_Booker.Migrations
{
    public partial class addingCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "Resorts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Resorts");
        }
    }
}
