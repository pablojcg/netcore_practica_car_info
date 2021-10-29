using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLUtilitiesMicroServices;
using CarInfo.Infraestructure.Entities;
using CarInfo.Infraestructure;
using CarInfo.Domain.PocoClass;

namespace CarInfo.Domain.QuerysDomain
{
    public class QueryCars
    {
        #region " [ Variables globales ] "

        private readonly CarInfoDataBaseContext dbContext;

        #endregion

        #region " [Constructor] "
        public QueryCars()
        {
            this.dbContext = new CarInfoDataBaseContext();
        }
		#endregion

		#region " [Querys] "
		//Consultar Tipos de carro
		public ResultModel<CarType> CarsTypeDomain(int id = 0, int page = 1, int items = 0)
		{
			ResultModel<CarType> result = new ResultModel<CarType>();

			try
			{
				if (id < 0)
				{
					throw new Exception("El identificador debe ser positivo");
				}

				GraphQLUtilitiesMicroServices.PageInfo pageInfo = null;
				result.custom = (id <= 0 ? dbContext.CarType : dbContext.CarType.Where(x => x.idCarType == id))
					.OrderBy(x => x.idCarType)
					.Pagination(page, items, ref pageInfo);
				result.PagesInfo = pageInfo;
			}
			catch (Exception ex)
			{
				result = new ResultModel<CarType>(ex);
			}
			return result;
		}

		public IEnumerable<CarModel> GetCarModelDomain() => dbContext.CarModel;

		public ResultModel<CarInfoModelTotal> CarsInfoModelTotalDomain(int id = 0, int page = 1, int items = 0) 
		{
			ResultModel<CarInfoModelTotal> result = new ResultModel<CarInfoModelTotal>();
			dynamic listCarsModel;
			List<CarInfoModelTotal> listCars = new List<CarInfoModelTotal>();
			CarInfoModelTotal carsSingle = null;

			if (id < 0)
			{
				throw new Exception("El identificador debe ser positivo");
			}

			GraphQLUtilitiesMicroServices.PageInfo pageInfo = null;

			try {
				if (id != 0) {
					listCarsModel = (from carsModel in dbContext.CarModel
									 join carsType in dbContext.CarType on carsModel.idCarType equals carsType.idCarType
									 join carsBrand in dbContext.CarBrand on carsModel.idCarBrand equals carsBrand.idCarBrand
									 where carsModel.idCarModel == id
									 orderby carsModel.idCarModel
									 select new
									 {
										 carsModel.idCarModel,
										 carsModel.nameModel,
										 carsModel.yearModel,
										 carsModel.price,
										 carsModel.gatesNumber,
										 carsModel.idCarBrand,
										 carsModel.idCarType,
										 carsType.nameType,
										 carsBrand.nameBrand
									 }).Pagination(page, items, ref pageInfo);
				}
				else {
					listCarsModel = (from carsModel in dbContext.CarModel
									 join carsType in dbContext.CarType on carsModel.idCarType equals carsType.idCarType
									 join carsBrand in dbContext.CarBrand on carsModel.idCarBrand equals carsBrand.idCarBrand
									 orderby carsModel.idCarModel
									 select new
									 {
										 carsModel.idCarModel,
										 carsModel.nameModel,
										 carsModel.yearModel,
										 carsModel.price,
										 carsModel.gatesNumber,
										 carsModel.idCarBrand,
										 carsModel.idCarType,
										 carsType.nameType,
										 carsBrand.nameBrand
									 }).Pagination(page, items, ref pageInfo);
				}

				foreach (var item in listCarsModel) 
				{
					carsSingle = new CarInfoModelTotal() { 
						idCarModel  = item.idCarModel,
						nameModel   = item.nameModel,
						yearModel   = item.yearModel,
						price       = item.price,
						gatesNumber = item.gatesNumber,
						idCarBrand  = item.idCarBrand,
						idCarType   = item.idCarType,
						nameType    = item.nameType,
						nameBrand   = item.nameBrand
					};

					listCars.Add(carsSingle);
				}
				result.custom3 = listCars;
				result.PagesInfo = pageInfo;

			}
			catch (Exception ex)
			{
				result = new ResultModel<CarInfoModelTotal>(ex);
			}

			return result;

		}

		#endregion
	}
}
