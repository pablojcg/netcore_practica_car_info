using Microsoft.EntityFrameworkCore.Migrations;

namespace CarInfo.Infraestructure.Migrations
{
    public partial class mg2321PC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "gatesNumber",
                table: "CarModel",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gatesNumber",
                table: "CarModel");
        }
    }
}
