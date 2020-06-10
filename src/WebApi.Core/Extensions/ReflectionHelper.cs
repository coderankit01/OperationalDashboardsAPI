using OperationalDashboard.Web.Api.Core.Constants;
using OperationalDashboard.Web.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Extensions
{
   public  class ReflectionHelper
    {
        public static IResourceDetails GetInstanceByNamespace(string nameSpace)
        {
            Type t = Type.GetType(MonitoringConstants.nameSpaceAndClassMapping[nameSpace]);
            return (IResourceDetails)Activator.CreateInstance(t);
        }
    }
}
