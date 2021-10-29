using Microsoft.EntityFrameworkCore.Migrations;

namespace CarInfo.Infraestructure.Migrations
{
    public partial class mgcamponuevoPC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CampoNuevo",
                table: "CarModel",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CampoNuevo",
                table: "CarModel");
        }
    }
}
