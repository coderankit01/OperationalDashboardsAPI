using Amazon.CostExplorer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
    public interface ICostExplorerRepository
    {
        Task<GetCostAndUsageResponse> GetCostAndUsage(GetCostAndUsageRequest costUsageRequest);
    }
}
