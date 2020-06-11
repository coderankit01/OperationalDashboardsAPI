using Amazon.AWSSupport.Model;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Infrastructure.Data.AWS;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
    public class TrustedAdvisorOperations: ITrustedAdvisorOperations
    {
        private static ITrustedAdvisorRepository trustedAdvisorRepository { get; set; }
        public TrustedAdvisorOperations(ITrustedAdvisorRepository _trustedAdvisorRepository)
        {
            trustedAdvisorRepository = _trustedAdvisorRepository;
        }
        public async Task<DescribeTrustedAdvisorChecksResponse> TrustedAdvisorChecks(string language)
        {
            trustedAdvisorRepository.Region = "us-east-1";
            DescribeTrustedAdvisorChecksRequest describeTrustedAdvisorChecksRequest = new DescribeTrustedAdvisorChecksRequest();
            describeTrustedAdvisorChecksRequest.Language = language;
            var response = await trustedAdvisorRepository.GetTrustedAdvisorChecks(describeTrustedAdvisorChecksRequest);
            return response;
        }
        public async Task<DescribeTrustedAdvisorCheckResultResponse> TrustedAdvisorCheckResult(string checkID, string language)
        {
            trustedAdvisorRepository.Region = "us-east-1";
            DescribeTrustedAdvisorCheckResultRequest describeTrustedAdvisorCheckResultRequest = new DescribeTrustedAdvisorCheckResultRequest();
            describeTrustedAdvisorCheckResultRequest.CheckId = checkID;
            describeTrustedAdvisorCheckResultRequest.Language = language;
            var response = await trustedAdvisorRepository.GetTrustedAdvisorCheckResult(describeTrustedAdvisorCheckResultRequest);
            return response;
        }
        public async Task<DescribeTrustedAdvisorCheckSummariesResponse> TrustedAdvisorCheckSummary(List<string> checkIDs)
        {
            trustedAdvisorRepository.Region = "us-east-1";
            DescribeTrustedAdvisorCheckSummariesRequest describeTrustedAdvisorCheckSummariesRequest = new DescribeTrustedAdvisorCheckSummariesRequest();
            describeTrustedAdvisorCheckSummariesRequest.CheckIds = checkIDs;
            var response = await trustedAdvisorRepository.GetTrustedAdvisorCheckSummary(describeTrustedAdvisorCheckSummariesRequest);
            return response;
        }

    }
}
