using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;
using OperationalDashboard.Web.Api.Infrastructure.Base;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
    public class CostExplorerRepository :AWSBaseClient, ICostExplorerRepository
    {
        public async Task<GetCostAndUsageResponse> GetCostAndUsage(GetCostAndUsageRequest costUsageRequest)
        {
             using (var amazonCostExplorerClient = new AmazonCostExplorerClient(awsCredentials, RegionEndpoint.GetBySystemName("us-east-1")))
            {
                var response = await amazonCostExplorerClient.GetCostAndUsageAsync(costUsageRequest);
                return response;
            }
               
        }
        public async Task<GetCostForecastResponse> GetCostForecast(GetCostForecastRequest costForecastRequest)
        {
            using (var amazonCostExplorerClient = new AmazonCostExplorerClient(awsCredentials, RegionEndpoint.GetBySystemName("us-east-1")))
            {
                var response = await amazonCostExplorerClient.GetCostForecastAsync(costForecastRequest);
                return response;
            }
        }

        public async Task<GetRightsizingRecommendationResponse> GetRightsizingRecommendation(GetRightsizingRecommendationRequest rightsizingRecommendationRequest)
        {
             using (var amazonCostExplorerClient = new AmazonCostExplorerClient(awsCredentials, RegionEndpoint.GetBySystemName("us-east-1")))
            {
                var response = await amazonCostExplorerClient.GetRightsizingRecommendationAsync(rightsizingRecommendationRequest);
                return response;
            }
        }
    }

}
