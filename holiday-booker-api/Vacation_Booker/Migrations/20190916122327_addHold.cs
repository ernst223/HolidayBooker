using Microsoft.EntityFrameworkCore.Migrations;

namespace Vacation_Booker.Migrations
{
    public partial class addHold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<bool>(
            //    name: "Hold",
            //    table: "Vacations",
            //    nullable: false,
            //    defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hold",
                table: "Vacations");
        }
    }
}
