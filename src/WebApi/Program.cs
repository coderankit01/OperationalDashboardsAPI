using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OperationDashboard.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Hack::To update environment variable in Elastic Beanstalk (As there is no direct Environments Variables read), Remove/Comment for other deployments Like Azure App services
            SetElasticBeanConfig();
            CreateHostBuilder(args).Build().Run();
        }
        private static void SetElasticBeanConfig()
        {
            var tempConfigBuilder = new ConfigurationBuilder();

            tempConfigBuilder.AddJsonFile(
                @"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration",
                optional: true,
                reloadOnChange: true
            );

            var configuration = tempConfigBuilder.Build();

            var ebEnv =
                configuration.GetSection("iis:env")
                    .GetChildren()
                    .Select(pair => pair.Value.Split(new[] { '=' }, 2))
                    .ToDictionary(keypair => keypair[0], keypair => keypair[1]);

            foreach (var keyVal in ebEnv)
            {
                Environment.SetEnvironmentVariable(keyVal.Key, keyVal.Value);
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
