using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInfo.Infraestructure.Entities;
using CarInfo.Domain.MutationsDomain;
using GraphQLUtilitiesMicroServices;
using HotChocolate.Types;
using CarInfo.Domain.PocoClass;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace CarInfo.API.GraphQL
{
    public class MutationsAPI
    {
        #region " [ Variables globales ] "
        public MutationCars domainMutations = null;
        #endregion

        #region " [ Contructor ] "
        public MutationsAPI()
        {
            this.domainMutations = new MutationCars();
        }
        #endregion

        #region " [ Mutations ] "
        public ResultModel<bool> AddUser(CreateCarTypeBrand input)
        {
            ResultModel<bool> result;
            return result = domainMutations.AddCarTypeBrand(input);
        }

        public async Task<ResultModel<CarType>> AddCarType(CarType input, [Service] ITopicEventSender topicEventSender)
        {
            ResultModel<CarType> result;
            result = domainMutations.AddCarTypeDomain(input);
            await topicEventSender.SendAsync("createCarType", "Se inserto en la Entidad CarType: " + result.custom2.idCarType);
            return result;
        }

        public ResultModel<bool> UpdateCarType(CarType input)
        {
            ResultModel<bool> result;
            return result = domainMutations.UpdateCarTypeDomain(input);
        }

        public ResultModel<bool> DeleteCarType(int idCarType)
        {
            ResultModel<bool> result;
            return result = domainMutations.DeleteCarTypeDomain(idCarType);
        }
        #endregion
    }
}
