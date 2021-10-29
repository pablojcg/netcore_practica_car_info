using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLUtilitiesMicroServices;
using CarInfo.Infraestructure.Entities;
using CarInfo.Infraestructure;
using CarInfo.Domain.PocoClass;

namespace CarInfo.Domain.MutationsDomain
{
    public class MutationCars
    {
        #region " [ Variables globales ] "

        private readonly CarInfoDataBaseContext dbContext;

        #endregion

        #region " [Constructor] "
        public MutationCars()
        {
            this.dbContext = new CarInfoDataBaseContext();
        }
        #endregion

        #region " [ Mutations ] "
        public ResultModel<bool> AddCarTypeBrand(CreateCarTypeBrand input) 
        {
            ResultModel<bool> result = new ResultModel<bool>();
            CarType carType = null;
            CarBrand carBrand = null;

            try {
                carType = new CarType() { 
                    nameType        = input.nameType,
                    descriptionType = input.descriptionType
                };

                carBrand = new CarBrand() { 
                    nameBrand    = input.nameBrand,
                    sinceBrand   = input.sinceBrand,
                    countryBrand = input.countryBrand
                };

                dbContext.CarType.Add(carType);
                dbContext.CarBrand.Add(carBrand);

                if (dbContext.SaveChanges() <= 0)
                {
                    result = new ResultModel<bool>(new Exception("Error trying to save values"));
                }
            }
            catch (Exception ex)
            {
                result = new ResultModel<bool>(ex);
            }

            return result;
        }

        public ResultModel<CarType> AddCarTypeDomain(CarType input) {

            ResultModel<CarType> result = new ResultModel<CarType>();
            try {
                dbContext.CarType.Add(input);
                if (dbContext.SaveChanges() <= 0)
                {
                    result = new ResultModel<CarType>(new Exception("Error trying to save values"));
                }
            }
            catch (Exception ex)
            {
                result = new ResultModel<CarType>(ex);

            }

            result.custom2 = input;

            return result;
        }

        public ResultModel<bool> UpdateCarTypeDomain(CarType input) 
        {
            ResultModel<bool> result = new ResultModel<bool>();

            if (input.idCarType < 0)
            {
                throw new Exception("El identificador debe ser positivo");
            }

            try
            {
                CarType value = dbContext.CarType.FirstOrDefault(x => x.idCarType == input.idCarType);
                if(value != null)
                {
                    value.nameType = (!String.IsNullOrEmpty(input.nameType)) ? input.nameType : value.nameType;
                    value.descriptionType = (!String.IsNullOrEmpty(input.descriptionType)) ? input.descriptionType : value.descriptionType;
                    dbContext.CarType.Update(value);
                    if (dbContext.SaveChanges() <= 0)
                    {
                        result = new ResultModel<bool>(new Exception("Error trying to update values"));
                    }
                }
                else 
                {
                    result = new ResultModel<bool>(new Exception($"Cannot find Direction by id {input.idCarType}"));
                }
            }
            catch (Exception ex)
            {
                result = new ResultModel<bool>(ex);
            }

            return result;
        }

        public ResultModel<bool> DeleteCarTypeDomain(int idCarType) {

            ResultModel<bool> result = new ResultModel<bool>();

            if (idCarType < 0)
            {
                throw new Exception("El identificador debe ser positivo");
            }

            try {

                CarType value = dbContext.CarType.FirstOrDefault(x => x.idCarType == idCarType);

                if(value != null)
                {
                    dbContext.CarType.Remove(value);
                    if (dbContext.SaveChanges() <= 0)
                    {
                        result = new ResultModel<bool>(new Exception("Error trying to delete values"));
                    }
                }
                else 
                {
                    result = new ResultModel<bool>(new Exception($"Cannot find Direction by id {idCarType}"));
                }
            }
            catch (Exception ex)
            {
                result = new ResultModel<bool>(ex);

            }
            return result;
        }
        #endregion
    }
}
