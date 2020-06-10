using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using OperationalDashboard.Web.Api.Infrastructure.Base;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
   public class CognitoIdentityProviderRepository:AWSBaseClient, ICognitoIdentityProviderRepository
    {
        public string Region { get; set; }
        public async Task<ListUserPoolsResponse> GetUserPool(ListUserPoolsRequest listUserPoolsRequest)
        {
            using (var amazonCognitoIdentityProviderClient= new AmazonCognitoIdentityProviderClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonCognitoIdentityProviderClient.ListUserPoolsAsync(listUserPoolsRequest);
                return response;
            }
        }
    }
}
