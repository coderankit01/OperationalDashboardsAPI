using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Infrastructure.Config
{
   public static class Configurations
    {
        public static IConfigurationRoot Settings { get; set; }
        public static string Profile { get{

                return Settings["Profile"];
            
            } }
    }
}
