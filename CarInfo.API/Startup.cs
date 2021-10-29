using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using CarInfo.API.GraphQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarInfo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static readonly string appName = "CarsInfo".ToUpper();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarInfo.API", Version = "v1" });
            });
            */

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = appName,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appName.PadLeft(16, '*').Substring(0, 16)))
                };
                options.Events = new JwtBearerEvents();

            });

            services.AddInMemorySubscriptions();

            services.AddGraphQL(provider => SchemaBuilder
                .New()
                .AddServices(provider)
                .AddQueryType<QuerysAPI>()
                .AddMutationType<MutationsAPI>()
                .AddSubscriptionType<SubscriptionAPI>()
                .AddAuthorizeDirectiveType()
                .Create()
            );

            services.AddQueryRequestInterceptor((ctx, builder, ct) =>
            {
                var headers = ctx.Request.Headers;
                builder.SetProperty("jwt", headers["jwt"].ToString());
                builder.SetProperty("realm", headers["realm"].ToString());
                return Task.CompletedTask;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarInfo.API v1"));
                app.UsePlayground(new PlaygroundOptions
                {
                    QueryPath = "/api/CarInfoAPI",
                    Path = "/playground"
                });
            }

            app.UseWebSockets();

            app.UseAuthentication();

            app.UseGraphQL("/api/CarInfoAPI");

            /*
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            */
        }
    }
}
