using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantLibrary.Migrations
{
    public partial class UserFirstnameandLastname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "bookedTables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "bookedTables",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "bookedTables");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "bookedTables");
        }
    }
}
