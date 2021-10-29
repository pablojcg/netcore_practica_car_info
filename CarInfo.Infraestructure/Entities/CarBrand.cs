using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarInfo.Infraestructure.Entities
{
    public class CarBrand
    {
        public CarBrand()
        {
            carModelHasCarBrand = new HashSet<CarModel>();
        }

        [Key]
        public int idCarBrand { get; set; }
        public string nameBrand { get; set; }
        public string sinceBrand { get; set; }
        public string countryBrand { get; set; }
        public string newCampoCarBrand { get; set; }
        public virtual ICollection<CarModel> carModelHasCarBrand { get; set; }
    }
}
