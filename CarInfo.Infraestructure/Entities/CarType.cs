using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarInfo.Infraestructure.Entities
{
    public class CarType
    {
        public CarType()
        {
            carModelHasCarType = new HashSet<CarModel>();
        }

        [Key]
        public int idCarType { get; set; }
        public string nameType { get; set; }
        public string descriptionType {get; set;}
        public virtual ICollection<CarModel> carModelHasCarType { get; set; }

    }

}
