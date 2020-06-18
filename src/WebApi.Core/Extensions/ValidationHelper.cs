using OperationalDashboard.Web.Api.Core.Constants;
using OperationalDashboard.Web.Api.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Extensions
{
   public static class ValidationHelper
    {
        public static bool IsValidCostUsageRequest(CostUsageRequest costUsageRequest, out  string message)
        {
            DateTime temp;
            if (!DateTime.TryParse(costUsageRequest.StartDate.ToString(), out temp))
            {
                message = $"Invalid Start Date:{costUsageRequest.StartDate.ToString()}";
                    return false;
            }
            if (!DateTime.TryParse(costUsageRequest.EndDate.ToString(), out temp))
            {
                message = $"Invalid End Date:{costUsageRequest.EndDate.ToString()}";
                return false;
            }
            if (!CostUsageConstants.granularity.Any(x=> x.Equals(costUsageRequest.Granularity)))
            {
                message =$"Granularity is invalid:{costUsageRequest.Granularity.ToString()}, Please provide a proper Granularity. Either MONTHLY or DAILY";
                return false;
            }
            if( !costUsageRequest.GroupBy.Any(x=> CostUsageConstants.GroupBy.Any(y=> y.Equals(x.Key))))
            //(!CostUsageConstants.GroupBy.Any(x=> x.Equals(costUsageRequest.GroupBy.FirstOrDefault().Key)))
            {
                message = $"Invalid GroupBy key value:{String.Join(",",costUsageRequest.GroupBy.Select(x=> x.Key))}, it should be { String.Join(",", CostUsageConstants.GroupBy)}";
                return false;

            }
            if (!CostUsageConstants.Filter.Any(x => x.Equals(costUsageRequest.Filters.Key)))
            {
                message = $"Invalid Filter key value:{costUsageRequest.Filters.Key}, it should be {String.Join(",", CostUsageConstants.Filter)}";
                return false;

            }
            if (!CostUsageConstants.Metrics.Any(x => x.Equals(costUsageRequest.Metrics)))
            {
                message = $"Invalid Metrics value:{costUsageRequest.Metrics}, it should be {String.Join(",", CostUsageConstants.Metrics)}";
                return false;

            }
            message = "The request is valid";
            return true;


        }
        public static bool IsValidMonitorRequest(MonitoringRequest monitoringRequest,string MetricType, out string Message)
        {
            if (monitoringRequest.ResourceIds==null|| !monitoringRequest.ResourceIds.Any())
            {
                
                Message = $"There is no ResourceId corresponding to the metrics";
                return false;
            }
            DateTime temp;
            if(monitoringRequest.IsDateCustom)
            {
                if (!DateTime.TryParse(monitoringRequest.StartDateTime.ToString(), out temp))
                {

                    Message = $"Invalid Start Date:{monitoringRequest.StartDateTime.ToString()}";
                    return false;
                }
                if (!DateTime.TryParse(monitoringRequest.EndDateTime.ToString(), out temp))
                {
                    Message = $"Invalid End Date:{monitoringRequest.EndDateTime.ToString()}";
                    return false;
                }
            }
            else
            {
                if (monitoringRequest.RelativeMinutes <= 0)
                {
                    Message = $"Invalid relative time:{monitoringRequest.RelativeMinutes}";
                    return false;
                }
            }
            if(!MonitoringConstants.MetricType.Any(x=> x.Equals(MetricType)))
            {
                Message=$"Invalid MetricType:{MetricType}, Please enter proper MetricType; It should be {String.Join(",", MonitoringConstants.MetricType.ToList())}";
                return false;
            }
            if(!MonitoringConstants.NameSpace.Any(x=> x.Equals(monitoringRequest.NameSpace)))
            {
                Message=$"Invalid namespace:{monitoringRequest.NameSpace}, Please provide a proper namespace; It should be {String.Join(",", MonitoringConstants.NameSpace.ToList())}";
                return false;
            }
            Message = "Request is valid";
            return true;
        }
        public static bool IsValidateGetMetrics(MonitoringRequest monitoringRequest, out string Message)
        {
            if (!MonitoringConstants.NameSpace.Any(x => x.Equals(monitoringRequest.NameSpace)))
            {
                Message = $"Invalid namespace:{monitoringRequest.NameSpace}, Please provide a proper namespace; It should be {String.Join(",", MonitoringConstants.NameSpace.ToList())}";
                return false;
            }
            if (!MonitoringConstants.Ec2Metrics.Any(x => x.Equals(monitoringRequest.Metrics)))
            {
                Message = $"Invalid Metrics value:{monitoringRequest.Metrics}, it should be {String.Join(",", MonitoringConstants.Ec2Metrics)}";
                return false;
            }
            Message = "Request is valid";
            return true;
        }   
        public static bool IsValidateSummary(MonitoringRequest monitoringRequest , out string Message)
        {
            if (!MonitoringConstants.NameSpace.Any(x => x.Equals(monitoringRequest.NameSpace)))
            {
                Message = $"Invalid namespace:{monitoringRequest.NameSpace}, Please provide a proper namespace; It should be {String.Join(",", MonitoringConstants.NameSpace.ToList())}";
                return false;
            }
            Message = "Request is valid";
            return true;
        }
        public static bool IsValidateResources(MonitoringResourceRequest monitoringResourceRequest, out string Message)
        {
            if(!MonitoringConstants.NameSpace.Any(X=> X.Equals(monitoringResourceRequest.Namespace)))
            {
                Message = $"Invalid Namespace:{monitoringResourceRequest.Namespace}, Please provide proper namespace. It should be {String.Join(",", MonitoringConstants.NameSpace.ToList())}";
                return false;
            }
            Message = "Request is valid";
            return true;
        }

            
    }
}
