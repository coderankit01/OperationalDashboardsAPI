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
        Task<GetCostForecastResponse> GetCostForecast(GetCostForecastRequest costForecastRequest);
        Task<GetRightsizingRecommendationResponse> GetRightsizingRecommendation(GetRightsizingRecommendationRequest rightsizingRecommendationRequest);
    }
}
