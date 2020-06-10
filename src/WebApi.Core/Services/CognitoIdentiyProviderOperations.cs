using Amazon.CognitoIdentityProvider.Model;
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
    public class CognitoIdentiyProviderOperations : IResourceDetails
    {
        private static ICognitoIdentityProviderRepository cognitoIdentityProviderRepository { get; set; }
        public CognitoIdentiyProviderOperations(ICognitoIdentityProviderRepository _cognitoIdentityProviderRepository)
        {
            cognitoIdentityProviderRepository = _cognitoIdentityProviderRepository;
        }
        public CognitoIdentiyProviderOperations()
        {

        }
        public async Task<object> GetResourceDetails(MonitoringResourceRequest monitoringResourceRequest)
        {
            cognitoIdentityProviderRepository = new CognitoIdentityProviderRepository()
            {
                region = monitoringResourceRequest.Region
            };
            var response = await cognitoIdentityProviderRepository.GetUserPool(new ListUserPoolsRequest());
            var filterResponse = response.UserPools.Where(x => monitoringResourceRequest.ResourceIds.Any(y => y.Equals(x.Name)));
            return filterResponse;
        }

        public async Task<MonitoringSummaryResponse> GetResources(string region)
        {
            cognitoIdentityProviderRepository = new CognitoIdentityProviderRepository()
            {
                region = region
            };
            var response = await cognitoIdentityProviderRepository.GetUserPool(new ListUserPoolsRequest());
            var resources = response.UserPools.Select(x => x.Name).ToList();
            return new MonitoringSummaryResponse()
            {
                Label = "AWS/Cognito",
                Count = resources.Count,
                ResourcesId = resources
            };
            throw new NotImplementedException();
        }
    }
}
