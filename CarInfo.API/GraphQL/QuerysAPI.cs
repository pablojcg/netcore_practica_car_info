using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInfo.Infraestructure.Entities;
using CarInfo.Domain.QuerysDomain;
using GraphQLUtilitiesMicroServices;
using HotChocolate.Types;
using CarInfo.Domain.PocoClass;
using HotChocolate.Resolvers;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HotChocolate.AspNetCore.Authorization;

namespace CarInfo.API.GraphQL
{
    public class QuerysAPI
    {
        #region " [ Variables globales ] "
        public QueryCars domainQuerys = null;
        #endregion

        #region " [ Contructor ] "
        public QuerysAPI() 
        {
            this.domainQuerys = new QueryCars();
        }
        #endregion

        #region " [ Querys Domain ] "
        public ResultModel<CarType> CarsTypes(IResolverContext context, int id = 0, int page = 1, int items = 0)
        {
            ResultModel<CarType> result;

            string jwt = context.CustomProperty<string>("jwt");
            string realm = context.CustomProperty<string>("realm");

            string resp = new KeyCloackAuth().VerifyTokenKeyCloak(jwt, realm);
            if (resp != "200") return result = new ResultModel<CarType>() { state = "601", error = "true", message = resp };
            
            result = domainQuerys.CarsTypeDomain(id, page, items);
            return result;
        }

        [Authorize]
        public ResultModel<CarInfoModelTotal> CarsModeltotal(int id = 0, int page = 1, int items = 0)
        {
            ResultModel<CarInfoModelTotal> result;
            result = domainQuerys.CarsInfoModelTotalDomain(id, page, items);
            return result;
        }


        [UseSelection]
        [UseFiltering]
        [UseSorting]
        public IEnumerable<CarModel> GetCarsModelGraph() => domainQuerys.GetCarModelDomain();

        public string Token(string appName = "CarsInfo")
        {
            appName = appName.ToUpper();
            IdentityModelEventSource.ShowPII = true;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appName.PadLeft(16, '*').Substring(0, 16)));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(appName, appName,
                new[] {
                    new Claim(JwtRegisteredClaimNames.UniqueName, appName + "_" + Guid.NewGuid().ToString())
                },
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );
            string x = new JwtSecurityTokenHandler().WriteToken(token);
            return x;
        }

        #endregion
    }
}
