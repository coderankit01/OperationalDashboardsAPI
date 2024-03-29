using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Mapper;
using OperationalDashboard.Web.Api.Core.Services;
using OperationalDashboard.Web.Api.Infrastructure.Config;
using OperationalDashboard.Web.Api.Infrastructure.Data.AWS;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using OperationDashboard.Web.Api.Extentions;
using OperationDashboard.Web.Api.Identity;


namespace OperationDashboard.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
         
            services.AddScoped<ICostExplorerRepository, CostExplorerRepository>();
            services.AddScoped<ICloudWatchRepository, CloudWatchRepository>();
            services.AddScoped<IEC2Repository, EC2Repository>();
            services.AddScoped<IEBSRepository, EBSRepository>();
            services.AddScoped<IDynamoDBRepository, DynamoDBRepository>();
            services.AddScoped<IAPIGatewayRepository, APIGatewayRepository>();
            services.AddScoped<IS3Repository, S3Repository>();
            services.AddScoped<ILambdaRepository, LambdaRepository>();
            services.AddScoped<IRDSRepostitory, RDSRepostitory>();
            services.AddScoped<ICognitoIdentityProviderRepository, CognitoIdentityProviderRepository>();
            services.AddScoped<ITrustedAdvisorRepository, TrustedAdvisorRepository>();
      

            services.AddScoped<ICostRecommendationsOperations, CachedCostRecommendationsOperations>();
            services.AddScoped<ICostExplorerOperations, CachedCostExplorerOperations>();
            services.AddScoped<CostExplorerOperations>();
            services.AddScoped<CostRecommendationsOperations>();
            services.AddScoped<IMonitoringOperations, CachedMonitoringOperations>();
            services.AddScoped<MonitoringOperations>();
            services.AddScoped<IEC2Operations, EC2Operations>();
            services.AddScoped<ITrustedAdvisorOperations, TrustedAdvisorOperations>();

            services.AddAutoMapper(typeof(AutoMapping));
            services.AddMemoryCache();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OperationalDashboardAPI", Version = "v1" });
                c.OperationFilter<SwaggerHeaderFilter>();
            });

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            
            Configurations.Settings = builder.Build();
            app.UseCors(builder => builder
                              .AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              );
            app.UseOptions();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OperationalDashboardAPI v1");
            });
            
            
            
        }
    }
}
