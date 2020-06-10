using Amazon;
using Amazon.APIGateway;
using Amazon.APIGateway.Model;
using OperationalDashboard.Web.Api.Infrastructure.Base;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
   public class APIGatewayRepository:AWSBaseClient, IAPIGatewayRepository
    {
        public string Region { get ; set ; }

        public async Task<GetRestApisResponse> GetApiDetails(GetRestApisRequest request)
        {
            using (var amazonAPIGatewayClient = new AmazonAPIGatewayClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonAPIGatewayClient.GetRestApisAsync(request);
                return response;
            }
        }
        public async Task<GetRestApisResponse> GetApiDetails()
        {
            using (var amazonAPIGatewayClient = new AmazonAPIGatewayClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                GetRestApisRequest request = new GetRestApisRequest();
                var response = await amazonAPIGatewayClient.GetRestApisAsync(request);
                return response;
            }
        }
    }
}
