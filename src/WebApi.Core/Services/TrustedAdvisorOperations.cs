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
        private async Task<DescribeTrustedAdvisorCheckResultResponse> TrustedAdvisorCheckResult(string checkID, string language)
        {
            trustedAdvisorRepository.Region = "us-east-1";
            DescribeTrustedAdvisorCheckResultRequest describeTrustedAdvisorCheckResultRequest = new DescribeTrustedAdvisorCheckResultRequest();
            describeTrustedAdvisorCheckResultRequest.CheckId = checkID;
            describeTrustedAdvisorCheckResultRequest.Language = language;
            var response = await trustedAdvisorRepository.GetTrustedAdvisorCheckResult(describeTrustedAdvisorCheckResultRequest);
            return response;
        }
        private async Task<DescribeTrustedAdvisorCheckSummariesResponse> TrustedAdvisorCheckSummary(List<string> checkIDs)
        {
            trustedAdvisorRepository.Region = "us-east-1";
            DescribeTrustedAdvisorCheckSummariesRequest describeTrustedAdvisorCheckSummariesRequest = new DescribeTrustedAdvisorCheckSummariesRequest();
            describeTrustedAdvisorCheckSummariesRequest.CheckIds = checkIDs;
            var response = await trustedAdvisorRepository.GetTrustedAdvisorCheckSummary(describeTrustedAdvisorCheckSummariesRequest);
            return response;
        }
        public async Task<AdvisorySummaryResponse> GetAdvisorySummary(string category)
        {
            var mapResponse = await GetCheckIDsByCategory(category);
            var stateResponse = await TrustedAdvisorCheckSummary(mapResponse);
            AdvisorySummaryResponse stateCountResponse = new AdvisorySummaryResponse()
            {
                WarningCount = stateResponse.Summaries.Where(x => x.Status.Equals("warning")).Count(),
                OkCount = stateResponse.Summaries.Where(x => x.Status.Equals("ok")).Count(),
                ErrorCount = stateResponse.Summaries.Where(x => x.Status.Equals("error")).Count(),
                NotAvailableCount = stateResponse.Summaries.Where(x => x.Status.Equals("not_available")).Count()
            };
            return stateCountResponse;
        }
        private async Task<List<string>> GetCheckIDsByCategory(string category)
        {
            var response = await TrustedAdvisorChecks("en");
            var mapResponse = response.Checks.Where(x => x.Category.Equals(category)).Select(y => y.Id).ToList();
            return mapResponse;
        }
        public async Task<List<ResourceRecommendationResponse>> GetResourceRecommendation(string category)
        {
            var response = await TrustedAdvisorChecks("en");
            var mapResponse = response.Checks.Where(x => x.Category.Equals(category)).Select(y => y.Id).ToList();
            var stateResponse = await TrustedAdvisorCheckSummary(mapResponse);
            var checkDictionaray = stateResponse.Summaries.Where(y=>y.Status.Equals("error")||y.Status.Equals("warning")).ToDictionary(x => x.CheckId, x => x);
            var currentResponse = response.Checks.Where(x => checkDictionaray.ContainsKey(x.Id)).Select(z => new ResourceRecommendationResponse()
            {
                CheckId=z.Id,
                CheckName = z.Name,
                Recommendation = z.Description,
                Status = checkDictionaray[z.Id].Status,
                ResourceCount = Convert.ToInt32(checkDictionaray[z.Id].ResourcesSummary?.ResourcesFlagged)
            }); 
           return currentResponse.ToList();
        }
        public async Task<object> GetResourceDetails(string checkID)
        {
            var response = await TrustedAdvisorChecks("en");
            var mapResponse = response.Checks.FirstOrDefault(x => x.Id.Equals(checkID));
            var details = response.Checks.Select(x => x.Metadata);
            var stateResponse = await TrustedAdvisorCheckResult(checkID,"en");
            var resourceDetails = stateResponse.Result.FlaggedResources.Select(x => x.Metadata.Select((z, i) => new { index = i, value = z }).ToDictionary(key => mapResponse.Metadata[key.index], value => value.value));
            return new
            {
                CheckName = mapResponse.Name,
                Recommendation = mapResponse.Description,
                Resources = resourceDetails
            };
            
        }
        public async Task<object> RefreshCheckByCategory(string category)
        {
            var checkIDs = await GetCheckIDsByCategory(category);
            var responses = new List<object>();
            foreach(var checkID in checkIDs)
            {
                var response = await trustedAdvisorRepository.RefreshChecks(new RefreshTrustedAdvisorCheckRequest() { CheckId = checkID });
                responses.Add(response);
            }
            return responses;
        }
    }
}
