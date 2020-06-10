using Amazon.APIGateway.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
   public interface IAPIGatewayRepository
    {
        string Region { get; set; }
        Task<GetRestApisResponse> GetApiDetails(GetRestApisRequest request);
        Task<GetRestApisResponse> GetApiDetails();
    }
}
