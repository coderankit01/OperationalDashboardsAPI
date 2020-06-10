using Amazon.Lambda.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
   public interface ILambdaRepository
    {
        string Region { get; set; }
        Task<ListFunctionsResponse> GetFunctions();
        Task<ListFunctionsResponse> GetFunctions(ListFunctionsRequest request);
    }
}
