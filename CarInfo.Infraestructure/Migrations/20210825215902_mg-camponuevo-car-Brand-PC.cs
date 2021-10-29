using Microsoft.EntityFrameworkCore.Migrations;

namespace CarInfo.Infraestructure.Migrations
{
    public partial class mgcamponuevocarBrandPC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "newCampoCarBrand",
                table: "CarBrand",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "newCampoCarBrand",
                table: "CarBrand");
        }
    }
}
