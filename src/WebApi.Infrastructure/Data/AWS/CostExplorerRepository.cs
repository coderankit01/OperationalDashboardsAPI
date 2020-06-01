using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
   public class CostExplorerRepository: ICostExplorerRepository
    {
        AmazonCostExplorerClient amazonCostExplorerClient = new AmazonCostExplorerClient("AKIA5SR5QSU3R352WQ6J", "yw4CqQmLlB7CdCKz06Cv2LIsWfLe9AQVvxk8HmKL", RegionEndpoint.GetBySystemName("ap-southeast-1"));
        public async Task<GetCostAndUsageResponse> GetCostAndUsage(GetCostAndUsageRequest costUsageRequest)
        {
            //var request = new GetCostAndUsageRequest()
            //{
            //    TimePeriod = new DateInterval() { Start = costUsageRequest.StartDate.ToString(), End = costUsageRequest.EndDate.ToString() },
            //    Metrics = costUsageRequest.Metrics,
            //    Granularity = Granularity.FindValue(costUsageRequest.Granularity),
            //    GroupBy = new List<GroupDefinition>() { new GroupDefinition() { Key = "LINKED_ACCOUNT", Type = "DIMENSION" }, new GroupDefinition() { Key = "SERVICE", Type = "DIMENSION" } }

            //};
            var response = await amazonCostExplorerClient.GetCostAndUsageAsync(costUsageRequest);
            return response;
        }

    }
}
