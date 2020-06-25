using Amazon.AWSSupport.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
   public interface ITrustedAdvisorRepository
    {
        string Region { get; set; }
        Task<DescribeTrustedAdvisorChecksResponse> GetTrustedAdvisorChecks(DescribeTrustedAdvisorChecksRequest describeTrustedAdvisorChecksRequest);
        Task<DescribeTrustedAdvisorCheckResultResponse> GetTrustedAdvisorCheckResult(DescribeTrustedAdvisorCheckResultRequest describeTrustedAdvisorCheckResultRequest);
        Task<DescribeTrustedAdvisorCheckSummariesResponse> GetTrustedAdvisorCheckSummary(DescribeTrustedAdvisorCheckSummariesRequest describeTrustedAdvisorCheckSummariesRequest);
        Task<RefreshTrustedAdvisorCheckResponse> RefreshChecks(RefreshTrustedAdvisorCheckRequest refreshTrustedAdvisorCheck);
    }
}
