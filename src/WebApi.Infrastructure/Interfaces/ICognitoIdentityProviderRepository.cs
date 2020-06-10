using Amazon.CognitoIdentityProvider.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
   public interface ICognitoIdentityProviderRepository
    {
        string Region { get; set; }
        Task<ListUserPoolsResponse> GetUserPool(ListUserPoolsRequest listUserPoolsRequest);
    }
}
