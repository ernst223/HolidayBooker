using Microsoft.EntityFrameworkCore.Migrations;

namespace Vacation_Booker.Migrations
{
    public partial class addingPartnerPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartnersPrice",
                table: "Vacations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultProfitOnStock",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartnersPrice",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "DefaultProfitOnStock",
                table: "AspNetUsers");
        }
    }
}
