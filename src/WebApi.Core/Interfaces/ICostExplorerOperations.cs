using Amazon.CostExplorer.Model;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Interfaces
{
    public interface ICostExplorerOperations
    {
        Task<List<CostUsageResponse>> GetCostUsage(CostUsageRequest costUsageRequest);
        Task<List<CostUsageResponse>> GetCurrentMonthCost(CostUsageRequest costUsageRequest);
        Task<List<CostUsageResponse>> GetCostByMonth(CostUsageRequest costUsageRequest);
        Task<List<CostUsageResponse>> GetCurrentYearCost(CostUsageRequest costUsageRequest);
        Task<List<CostUsageResponse>> GetCostForecast(CostUsageRequest costUsageRequest);
        Task<List<CostUsageResponse>> GetLinkedAccounts();


    }
}
