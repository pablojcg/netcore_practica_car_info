using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarInfo.Infraestructure.Entities
{
    public class CarModel
    {
        [Key]
        public int idCarModel { get; set; }
        public string nameModel { get; set; }
        public string yearModel { get; set; }
        public double price { get; set; }
        public int? gatesNumber { get; set; }
        public int idCarBrand { get; set; }
        public int idCarType { get; set; }
        public string CampoNuevo { get; set; }
        public virtual CarBrand carBrand { get; set; }
        public virtual CarType carType { get; set; }

    }
}
