using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
    public class CostRecommendationSummaryResponse
    {
        public int TotalInstance { get; set; }
        public double CurrentCost { get; set; }
        public double RecommendedCost { get; set; }
        public double Savings { get; set; }

    }
}
