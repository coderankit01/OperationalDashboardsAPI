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
        public async Task<List<CostRecommendationResponse>> GetCostSummaryDetails()
        {
            GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
            rightsizingRecommendationRequest.Service = "AmazonEC2";
            var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
            var recommendationResponse = response.RightsizingRecommendations.Select(x => new CostRecommendationResponse
            {
                ResourceId = x.CurrentInstance?.ResourceId,
                Cost = Convert.ToDecimal(x.CurrentInstance?.MonthlyCost),
                RecommendCost = Convert.ToDecimal(x.ModifyRecommendationDetail?.TargetInstances?.FirstOrDefault()?.EstimatedMonthlyCost),
                //TotalMonthlySavings = Convert.ToDecimal(x.CurrentInstance.MonthlyCost) - Convert.ToDecimal(x.ModifyRecommendationDetail.TargetInstances.FirstOrDefault().EstimatedMonthlyCost)
                Size  = x.CurrentInstance?.ResourceDetails?.EC2ResourceDetails?.InstanceType,
                RecommendSize = x.ModifyRecommendationDetail.TargetInstances.FirstOrDefault()?.ResourceDetails?.EC2ResourceDetails?.InstanceType
            });
            return recommendationResponse.ToList();
        }
        public async Task<object> GetCostVsSavings()
        {
            GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
            rightsizingRecommendationRequest.Service = "AmazonEC2";
            var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
            var recommendationResponse = response.RightsizingRecommendations.Select(x => new 
            {
                ResourceId = x.CurrentInstance.ResourceId,
                Cost = Convert.ToDecimal(x.CurrentInstance.MonthlyCost),
                Saving = Convert.ToDecimal(x.ModifyRecommendationDetail.TargetInstances.FirstOrDefault().EstimatedMonthlyCost)
            });
            return recommendationResponse;
        }
        public async Task<CostRecommendationSummaryResponse> GetCostSummary()
        {
            GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
            rightsizingRecommendationRequest.Service = "AmazonEC2";
            var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
            var currentCost = response.RightsizingRecommendations.Select(x => Convert.ToDouble(string.IsNullOrEmpty(x.CurrentInstance?.MonthlyCost)?"0": x.CurrentInstance?.MonthlyCost)).Sum();
            return new CostRecommendationSummaryResponse()
            {
                TotalInstance = Convert.ToInt32(string.IsNullOrEmpty(response.Summary?.TotalRecommendationCount)?"0": response.Summary?.TotalRecommendationCount),
                Savings = Math.Round(Convert.ToDouble(string.IsNullOrEmpty(response.Summary?.EstimatedTotalMonthlySavingsAmount)?"0": response.Summary?.EstimatedTotalMonthlySavingsAmount),2),
                RecommendedCost= Math.Round(currentCost - Convert.ToDouble(string.IsNullOrEmpty(response.Summary?.EstimatedTotalMonthlySavingsAmount) ? "0" : response.Summary?.EstimatedTotalMonthlySavingsAmount),2),
                CurrentCost=Math.Round( currentCost,2)


            };
        }
        public async Task<object> GetUsedVsUnusedCpu()
        {
            GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
            rightsizingRecommendationRequest.Service = "AmazonEC2";
            var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
            var cpuUsageDetails = response.RightsizingRecommendations.Select(x => new 
            {
                ResourceId = x.CurrentInstance.ResourceId,
                UnusedCPUusage = 100 - Convert.ToDecimal(x.CurrentInstance.ResourceUtilization.EC2ResourceUtilization.MaxCpuUtilizationPercentage),
                CPUusage = Convert.ToDecimal(x.CurrentInstance.ResourceUtilization.EC2ResourceUtilization.MaxCpuUtilizationPercentage),
            });
            return cpuUsageDetails.ToList();
        }
        public async Task<object> GetCpuVsRecommendCpuUsage()
        {
            GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
            rightsizingRecommendationRequest.Service = "AmazonEC2";
            var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
            var cpuUsageDetails = response.RightsizingRecommendations.Select(x => new 
            {
                ResourceId = x.CurrentInstance.ResourceId,
                RecommendCpu =  Convert.ToDecimal(x.ModifyRecommendationDetail.TargetInstances.FirstOrDefault().ExpectedResourceUtilization.EC2ResourceUtilization.MaxCpuUtilizationPercentage),
                CPUusage = Convert.ToDecimal(x.CurrentInstance.ResourceUtilization.EC2ResourceUtilization.MaxCpuUtilizationPercentage),
            });
            return cpuUsageDetails;
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
        //public async Task<CostRecommendationResponse> GetSummaryData()
        //{
        //    GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
        //    rightsizingRecommendationRequest.Service = "AmazonEC2";
        //    var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
        //    CostRecommendationResponse costRecommendationResponse = new CostRecommendationResponse()
        //    {
        //        TotalRecomCount = Convert.ToInt32(response.Summary.TotalRecommendationCount),
        //        EstimatedTotalMonthlySavingsAmount= Convert.ToDecimal(response.Summary.EstimatedTotalMonthlySavingsAmount),
        //        SavingsPercentage = Convert.ToDecimal(response.Summary.SavingsPercentage)
        //     };
        //    return costRecommendationResponse;
        //}
    }
}



    

