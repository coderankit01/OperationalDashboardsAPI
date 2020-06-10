using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using OperationalDashboard.Web.Api.Infrastructure.Base;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
   public class LambdaRepository:AWSBaseClient, ILambdaRepository
    {
        public string Region { get; set; }
        public async Task<ListFunctionsResponse> GetFunctions()
        {
            using (var amazonLambdaClient = new AmazonLambdaClient(awsCredentials,RegionEndpoint.GetBySystemName( Region)))
            {
                var response = await amazonLambdaClient.ListFunctionsAsync();
                return response;
            }
        }
        public async Task<ListFunctionsResponse> GetFunctions(ListFunctionsRequest request)
        {
            using (var amazonLambdaClient = new AmazonLambdaClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonLambdaClient.ListFunctionsAsync(request);
                return response;
            }
        }
    }
}
