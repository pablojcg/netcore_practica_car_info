using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarInfo.Infraestructure.Entities;
using Microsoft.Extensions.Configuration;

namespace CarInfo.Infraestructure
{
    public partial class CarInfoDataBaseContext : DbContext
    {
        #region " [ Variables globales ] "

        private string ConnectionString = string.Empty;

        #endregion

        #region " [ Constructor e inicializador de variables ] "

        public CarInfoDataBaseContext() 
        {
            /*
            IConfiguration config = new ConfigurationBuilder()
             .AddUserSecrets("fc21f59d-121a-4a8a-9f14-94d4f32659aa") //Nombre de la carpeta que hemos creado
             .Build();
            */
            var USERDB = Environment.GetEnvironmentVariable("USERDB");
            var PASSWORDDB = Environment.GetEnvironmentVariable("PASSWORDDB");
            var HOSTDB = Environment.GetEnvironmentVariable("HOSTDB");
            var PORTDB = Environment.GetEnvironmentVariable("PORTDB");
            var DATABSENAME = Environment.GetEnvironmentVariable("DATABSENAME");
            
            //this.ConnectionString = "User ID =postgres;Password=pablojjcg23;Server=172.17.0.1;Port=5432;Database=CarsInfDataBase;Integrated Security=true;Pooling=true;";
            this.ConnectionString = "User ID =postgres;Password=pablojjcg23;Server=localhost;Port=5432;Database=CarsInfDataBase;Integrated Security=true;Pooling=true;";

            //this.ConnectionString = config["ConnectionsString"];
            //this.ConnectionString = "User ID ="+ USERDB + ";Password="+ PASSWORDDB + ";Server="+ HOSTDB + ";Port="+ PORTDB + ";Database="+ DATABSENAME + ";Integrated Security=true;Pooling=true;";
            //Console.WriteLine(this.ConnectionString);
        }

        #endregion

        #region " [ Definición inicial de tablas DbSet ] "
        public virtual DbSet<CarType> CarType { get; set; }
        public virtual DbSet<CarBrand> CarBrand { get; set; }
        public virtual DbSet<CarModel> CarModel { get; set; }
        #endregion

        #region " [ Creacion de modelo de tablas y relaciones ] "
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<CarType>(entity => {

                entity.ToTable("CarType");

                entity.Property(e => e.nameType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.descriptionType)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.HasData(
                    new CarType() { idCarType = 1, nameType = "Familiar", descriptionType = "Son aquellos turismos derivados de compactos, sedanes o berlinas con carrocería de cinco puertas y techo elevado, a fin de ampliar el compartimento de carga, es decir, poseen una carrocería familiar o lo que comúnmente llamaríamos 'ranchera'" },
                    new CarType() { idCarType = 2, nameType = "Deportivo", descriptionType = "Dentro de este tipo de vehículos encontramos diversos tipos de carrocerías, pero todos ellos se caracterizan por equipar diseños realmente atractivos, motores muy potentes, una velocidad máxima que supera los 250 km/h y una tracción increíble enfocada a darte mayores prestaciones sobre la pista." },
                    new CarType() { idCarType = 3, nameType = "Subcompacto", descriptionType = "Este tipo de coches pueden tener tres, cuatro o cinco puertas, dependiendo un poco del diseño de su carrocería. Están diseñados para que puedan viajar cuatro pasajeros cómodamente y se corresponden con lo que conocemos como segmento B -aproximadamente de 3,9 a 4,3 metros de longitud-." },
                    new CarType() { idCarType = 4, nameType = "Urbano", descriptionType = "En el segmento de los urbanos podemos encontrar una gran variedad de modelos de distinto tamaño. En este grupo podríamos meter desde los micro-coches o pequeños utilitarios" }
                 );

            });

            modelBuilder.Entity<CarBrand>(entity => {

                entity.ToTable("CarBrand");

                entity.Property(e => e.nameBrand)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.sinceBrand)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.countryBrand)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.newCampoCarBrand)
                    .HasMaxLength(100);

                entity.HasData(
                    new CarBrand() { idCarBrand = 1, nameBrand = "Toyota", sinceBrand = "1937", countryBrand = "Japón" },
                    new CarBrand() { idCarBrand = 2, nameBrand = "Ford",  sinceBrand = "1903", countryBrand = "Estados Unidos (USA)" },
                    new CarBrand() { idCarBrand = 3, nameBrand = "Chevrolet", sinceBrand = "1911", countryBrand = "Estados Unidos (USA)" },
                    new CarBrand() { idCarBrand = 4, nameBrand = "Audi", sinceBrand = "1909", countryBrand = "Alemania" }
                );

            });

            modelBuilder.Entity<CarModel>(entity =>
            {
                entity.ToTable("CarModel");

                entity.Property(e => e.nameModel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.yearModel)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.price)
                    .IsRequired();

                entity.HasOne(d => d.carBrand)
                    .WithMany(p => p.carModelHasCarBrand)
                    .HasForeignKey(d => d.idCarBrand)
                    .HasConstraintName("FK_CarModelToCarBrand");

                entity.HasOne(d => d.carType)
                    .WithMany(p => p.carModelHasCarType)
                    .HasForeignKey(d => d.idCarType)
                    .HasConstraintName("FK_CarModelToCarType");

                entity.HasData(
                    new CarModel() { idCarModel = 1, nameModel = "Corolla Hybrid LE", yearModel = "2021", price = 22000.0, idCarType = 1, idCarBrand = 1 },
                    new CarModel() { idCarModel = 2, nameModel = "RAV4 Hybrid", yearModel = "2021", price = 28800.0, idCarType = 3, idCarBrand = 1 },
                    new CarModel() { idCarModel = 3, nameModel = "Camaro Six SS", yearModel = "2021", price = 50000.0, idCarType = 2, idCarBrand = 3 },
                    new CarModel() { idCarModel = 4, nameModel = "Beat", yearModel = "2021", price = 41890.0, idCarType = 3, idCarBrand = 2 },
                    new CarModel() { idCarModel = 6, nameModel = "MustangShelby GT350", yearModel = "2021", price = 64870.0, idCarType = 2, idCarBrand = 2 },
                    new CarModel() { idCarModel = 7, nameModel = "R8 Coupé", yearModel = "2021", price = 200000.0, idCarType = 2, idCarBrand = 4 },
                    new CarModel() { idCarModel = 8, nameModel = "Q8", yearModel = "2021", price = 85785.0, idCarType = 3, idCarBrand = 4 }
                );


            });

            OnModelCreatingPartial(modelBuilder);
        }
        #endregion

        #region " [Configuración para conexión del contexto] "
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(this.ConnectionString))
                {
                    throw new System.Exception("Connection is not defined.");
                }
                optionsBuilder.UseNpgsql(this.ConnectionString);
            }
        }
        #endregion

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
