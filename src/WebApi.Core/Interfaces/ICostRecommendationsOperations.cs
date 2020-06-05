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
        Task<List<CostRecommendationResponse>> GetCurrentCostRecomNSavingsCost();
        Task<List<CostRecommendationResponse>> GetCPUusageDetails();
        Task<GetRightsizingRecommendationResponse> GetHighRecommActivities();
        Task<CostRecommendationResponse> GetSummaryData();
    }
}
