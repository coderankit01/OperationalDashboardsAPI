using Amazon.APIGateway.Model;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Core.Models.Response;
using OperationalDashboard.Web.Api.Infrastructure.Data.AWS;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
    public class ApiGatewayOperations : IResourceDetails
    {
        private static IAPIGatewayRepository aPIGatewayRepository { get; set; }
        public ApiGatewayOperations(IAPIGatewayRepository _aPIGatewayRepository)
        {
            aPIGatewayRepository = _aPIGatewayRepository;
        }
        public async Task<object> GetResourceDetails(MonitoringResourceRequest monitoringResourceRequest)
        {
            aPIGatewayRepository = new APIGatewayRepository();
            aPIGatewayRepository.Region = monitoringResourceRequest.Region;
            var response = await aPIGatewayRepository.GetApiDetails();
            var filterResponse = response.Items.Where(x => monitoringResourceRequest.ResourceIds.Any(y => y.Equals(x.Name)));
            return filterResponse;
        }

        public async Task<MonitoringSummaryResponse> GetResources(string region)
        {
            aPIGatewayRepository = new APIGatewayRepository();
            aPIGatewayRepository.Region = region;
            var response =  await aPIGatewayRepository.GetApiDetails();
            var resources = response.Items.Select(x => x.Name).ToList();
            return new MonitoringSummaryResponse()
            {
                Label = "AWS/ApiGateway",
                Count = resources.Count,
                ResourcesId = resources
            };
        }
    }
}
