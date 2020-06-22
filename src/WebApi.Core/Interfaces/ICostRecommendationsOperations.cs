using Amazon.CostExplorer.Model;
using OperationalDashboard.Web.Api.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Interfaces
{
    public interface ICostRecommendationsOperations
    {
        Task<List<CostRecommendationResponse>> GetCostSummaryDetails();
        Task<CostRecommendationSummaryResponse> GetCostSummary();
        Task<object> GetCpuVsRecommendCpuUsage();
        Task<object> GetUsedVsUnusedCpu();
        Task<object> GetCostVsSavings();
        Task<GetRightsizingRecommendationResponse> GetHighRecommActivities();
    }
}
