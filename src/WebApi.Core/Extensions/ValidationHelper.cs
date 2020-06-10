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
            if( costUsageRequest.GroupBy.Any(x=> CostUsageConstants.GroupBy.Any(y=> y.Equals(x.Key))))
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

    }
}
