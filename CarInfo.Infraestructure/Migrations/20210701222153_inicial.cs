using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CarInfo.Infraestructure.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarBrand",
                columns: table => new
                {
                    idCarBrand = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameBrand = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    sinceBrand = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    countryBrand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrand", x => x.idCarBrand);
                });

            migrationBuilder.CreateTable(
                name: "CarType",
                columns: table => new
                {
                    idCarType = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descriptionType = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarType", x => x.idCarType);
                });

            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    idCarModel = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameModel = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    yearModel = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    idCarBrand = table.Column<int>(type: "integer", nullable: false),
                    idCarType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModel", x => x.idCarModel);
                    table.ForeignKey(
                        name: "FK_CarModelToCarBrand",
                        column: x => x.idCarBrand,
                        principalTable: "CarBrand",
                        principalColumn: "idCarBrand",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarModelToCarType",
                        column: x => x.idCarType,
                        principalTable: "CarType",
                        principalColumn: "idCarType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarBrand",
                columns: new[] { "idCarBrand", "countryBrand", "nameBrand", "sinceBrand" },
                values: new object[,]
                {
                    { 1, "Japón", "Toyota", "1937" },
                    { 2, "Estados Unidos (USA)", "Ford", "1903" },
                    { 3, "Estados Unidos (USA)", "Chevrolet", "1911" },
                    { 4, "Alemania", "Audi", "1909" }
                });

            migrationBuilder.InsertData(
                table: "CarType",
                columns: new[] { "idCarType", "descriptionType", "nameType" },
                values: new object[,]
                {
                    { 1, "Son aquellos turismos derivados de compactos, sedanes o berlinas con carrocería de cinco puertas y techo elevado, a fin de ampliar el compartimento de carga, es decir, poseen una carrocería familiar o lo que comúnmente llamaríamos 'ranchera'", "Familiar" },
                    { 2, "Dentro de este tipo de vehículos encontramos diversos tipos de carrocerías, pero todos ellos se caracterizan por equipar diseños realmente atractivos, motores muy potentes, una velocidad máxima que supera los 250 km/h y una tracción increíble enfocada a darte mayores prestaciones sobre la pista.", "Deportivo" },
                    { 3, "Este tipo de coches pueden tener tres, cuatro o cinco puertas, dependiendo un poco del diseño de su carrocería. Están diseñados para que puedan viajar cuatro pasajeros cómodamente y se corresponden con lo que conocemos como segmento B -aproximadamente de 3,9 a 4,3 metros de longitud-.", "Subcompacto" },
                    { 4, "En el segmento de los urbanos podemos encontrar una gran variedad de modelos de distinto tamaño. En este grupo podríamos meter desde los micro-coches o pequeños utilitarios", "Urbano" }
                });

            migrationBuilder.InsertData(
                table: "CarModel",
                columns: new[] { "idCarModel", "idCarBrand", "idCarType", "nameModel", "price", "yearModel" },
                values: new object[,]
                {
                    { 1, 1, 1, "Corolla Hybrid LE", 22000.0, "2021" },
                    { 3, 3, 2, "Camaro Six SS", 50000.0, "2021" },
                    { 6, 2, 2, "MustangShelby GT350", 64870.0, "2021" },
                    { 7, 4, 2, "R8 Coupé", 200000.0, "2021" },
                    { 2, 1, 3, "RAV4 Hybrid", 28800.0, "2021" },
                    { 4, 2, 3, "Beat", 41890.0, "2021" },
                    { 8, 4, 3, "Q8", 85785.0, "2021" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_idCarBrand",
                table: "CarModel",
                column: "idCarBrand");

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_idCarType",
                table: "CarModel",
                column: "idCarType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarModel");

            migrationBuilder.DropTable(
                name: "CarBrand");

            migrationBuilder.DropTable(
                name: "CarType");
        }
    }
}
