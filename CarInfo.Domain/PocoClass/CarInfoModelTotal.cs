using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarInfo.Infraestructure.Entities;

namespace CarInfo.Domain.PocoClass
{
    public class CarInfoModelTotal : CarModel
    {
        public string nameBrand { get; set; }
        public string nameType { get; set; }
    }
}
