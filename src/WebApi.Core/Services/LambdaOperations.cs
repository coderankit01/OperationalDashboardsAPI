using Amazon.Lambda.Model;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Core.Models.Response;
using OperationalDashboard.Web.Api.Infrastructure.Data.AWS;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
    public class LambdaOperations : IResourceDetails
    {
        private static ILambdaRepository lambdaRepository{ get; set; }
        public LambdaOperations(ILambdaRepository _lambdaRepository)
        {
            lambdaRepository = _lambdaRepository;
        }
        public LambdaOperations()
        {

        }
        public async Task<object> GetResourceDetails(MonitoringResourceRequest monitoringResourceRequest)
        {
            lambdaRepository = new LambdaRepository();
            lambdaRepository.Region = monitoringResourceRequest.Region;
            var response = await lambdaRepository.GetFunctions();
            var filterresponse = response.Functions.Where(x => monitoringResourceRequest.ResourceIds.Any(y => y.Equals(x.FunctionName))).Select(y=>new LambdaResponse() { 
                    FunctionName=y.FunctionName,
                    CodeSize=y.CodeSize.ToString(),
                    Description=y.Description,
                    LastModified=y.LastModified,
                    Runtime=y.Runtime
            
            
            } );
            return filterresponse;
        }

        public async Task<MonitoringSummaryResponse> GetResources(string region)
        {
            lambdaRepository = new LambdaRepository();
            lambdaRepository.Region = region;
            var response = await lambdaRepository.GetFunctions();
            var resources = response.Functions.Select(x => x.FunctionName).ToList();
            return new MonitoringSummaryResponse()
            {
                Label = "AWS/Lambda",
                Count = resources.Count,
                ResourcesId = resources
            };
        }
    }
}
