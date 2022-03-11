using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestKitManager.Migrations
{
    public partial class AddConfigFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfigFileContent",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfigFileName",
                table: "Services",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigFileContent",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ConfigFileName",
                table: "Services");
        }
    }
}
