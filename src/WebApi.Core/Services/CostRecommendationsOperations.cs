using Amazon.CostExplorer.Model;
using AutoMapper;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Response;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
    public class CostRecommendationsOperations : ICostRecommendationsOperations
    {

        private static IMapper mapper { get; set; }
        private static ICostExplorerRepository costExplorerRepository { get; set; }
        public CostRecommendationsOperations(IMapper _mapper, ICostExplorerRepository _costExplorerRepository)
        {
            mapper = _mapper;
            costExplorerRepository = _costExplorerRepository;
        }
        public async Task<List<CostRecommendationResponse>> GetCurrentCostRecomNSavingsCost()
        {
            GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
            rightsizingRecommendationRequest.Service = "AmazonEC2";
            var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
            var recommendationResponse = response.RightsizingRecommendations.Select(x => new CostRecommendationResponse
            {
                ResourceId = x.CurrentInstance.ResourceId,
                CurrentMonthlytotalCost = Convert.ToDecimal(x.CurrentInstance.MonthlyCost),
                RecommendedmonthlyEstimationCost = Convert.ToDecimal(x.ModifyRecommendationDetail.TargetInstances.FirstOrDefault().EstimatedMonthlyCost),
                TotalMonthlySavings = Convert.ToDecimal(x.CurrentInstance.MonthlyCost) - Convert.ToDecimal(x.ModifyRecommendationDetail.TargetInstances.FirstOrDefault().EstimatedMonthlyCost)
            });
            return recommendationResponse.ToList();
        }

        //2. 3.
        public async Task<List<CostRecommendationResponse>> GetCPUusageDetails()
        {
            GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
            rightsizingRecommendationRequest.Service = "AmazonEC2";
            var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
            var cpuUsageDetails = response.RightsizingRecommendations.Select(x => new CostRecommendationResponse
            {
                ResourceId = x.CurrentInstance.ResourceId,
                UnusedCurrentCPUusage = 100 - Convert.ToDecimal(x.CurrentInstance.ResourceUtilization
                .EC2ResourceUtilization.MaxCpuUtilizationPercentage),
                CurrentCPUusage = Convert.ToDecimal(x.CurrentInstance.ResourceUtilization.EC2ResourceUtilization.MaxCpuUtilizationPercentage),
                MaxRecommendedCPU = Convert.ToDecimal(x.ModifyRecommendationDetail.TargetInstances.FirstOrDefault().ExpectedResourceUtilization.EC2ResourceUtilization.MaxCpuUtilizationPercentage)
            });
            return cpuUsageDetails.ToList();
        }

        public async Task<GetRightsizingRecommendationResponse> GetHighRecommActivities()
        {
            GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
            rightsizingRecommendationRequest.Service = "AmazonEC2";
            var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
            var highRecomActivities = response.RightsizingRecommendations.SelectMany(x => x.ModifyRecommendationDetail.TargetInstances).Select(y => new { y.ResourceDetails.EC2ResourceDetails.InstanceType, y.ResourceDetails.EC2ResourceDetails.Memory, y.ResourceDetails.EC2ResourceDetails.Vcpu });
            return response;
        }

        //Summary data
        public async Task<CostRecommendationResponse> GetSummaryData()
        {
            GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
            rightsizingRecommendationRequest.Service = "AmazonEC2";
            var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
            CostRecommendationResponse costRecommendationResponse = new CostRecommendationResponse()
            {
                TotalRecomCount = Convert.ToInt32(response.Summary.TotalRecommendationCount),
                EstimatedTotalMonthlySavingsAmount= Convert.ToDecimal(response.Summary.EstimatedTotalMonthlySavingsAmount),
                SavingsPercentage = Convert.ToDecimal(response.Summary.SavingsPercentage)
             };
            return costRecommendationResponse;
        }
    }
}



    

