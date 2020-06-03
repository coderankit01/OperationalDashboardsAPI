using Amazon.CostExplorer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Interfaces
{
    public interface ICostRecommendationsOperations
    {
        Task<GetRightsizingRecommendationResponse> GetRightsizingRecommendation();
    }
}
