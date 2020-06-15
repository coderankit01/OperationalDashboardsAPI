using Amazon.AWSSupport.Model;
using OperationalDashboard.Web.Api.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Interfaces
{
    public interface ITrustedAdvisorOperations
    {
        Task<DescribeTrustedAdvisorChecksResponse> TrustedAdvisorChecks(string language);
        Task<List<ResourceRecommendationResponse>> GetResourceRecommendation(string category);
        Task<AdvisorySummaryResponse> GetAdvisorySummary(string category);
        Task<object> GetResourceDetails(string checkID);
    }
}
