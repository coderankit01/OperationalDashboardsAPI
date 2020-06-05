using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
    public class CostRecommendationResponse
    {
        public string ResourceId { get; set; }
        public decimal UnusedCurrentCPUusage { get; set; }
        public decimal CurrentMonthlytotalCost { get; set; }
        public decimal RecommendedmonthlyEstimationCost { get; set; }
        public decimal TotalMonthlySavings { get; set; }
        public decimal CurrentCPUusage { get; set; }
        public decimal MaxRecommendedCPU { get; set; }
        public int TotalRecomCount { get; set; }
        public decimal EstimatedTotalMonthlySavingsAmount { get; set; }
        public decimal SavingsPercentage { get; set; }
    }
}
