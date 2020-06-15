using Amazon.AWSSupport.Model;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Response;
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
    public class TrustedAdvisorOperations : ITrustedAdvisorOperations
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
        public async Task<StateCountResponse> GetStateCount(string Category)
        {
            var response = await TrustedAdvisorChecks("en");
            var mapResponse = response.Checks.Where(x => x.Category.Equals(Category)).Select(y => y.Id).ToList();
            var stateResponse = await TrustedAdvisorCheckSummary(mapResponse);
            StateCountResponse stateCountResponse = new StateCountResponse()
            {
                WarningCount = stateResponse.Summaries.Where(x => x.Status.Equals("warning")).Count(),
                OkCount = stateResponse.Summaries.Where(x => x.Status.Equals("ok")).Count(),
                ErrorCount = stateResponse.Summaries.Where(x => x.Status.Equals("critical")).Count(),
                NotAvailableCount = stateResponse.Summaries.Where(x => x.Status.Equals("not_available")).Count()
            };
            return stateCountResponse;
        }
        public async Task<List<CheckRecommResponse>> GetRecommendations(string Category)
        {
            var response = await TrustedAdvisorChecks("en");
            var mapResponse = response.Checks.Where(x => x.Category.Equals(Category)).Select(y => y.Id).ToList();
            var stateResponse = await TrustedAdvisorCheckSummary(mapResponse);
            var temp = stateResponse.Summaries.Where(x => x.Status.Equals("warning") || x.Status.Equals("error")).Select(y => y.CheckId).ToList();
            var checkDictionaray = stateResponse.Summaries.ToDictionary(x => x.CheckId, x => x.ResourcesSummary?.ResourcesFlagged);
            var currentResponse = response.Checks.Where(x => temp.Any(y => y.Equals(x.Id))).Select(z => new CheckRecommResponse()
            {
                CheckName = z.Name,
                Recommendation = z.Description,
                ResourceCount= Convert.ToInt32(checkDictionaray[z.Id])
            });

            
           return currentResponse.ToList();
        }
    }
}
