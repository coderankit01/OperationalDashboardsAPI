using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
    public class CostRecommendationResponse
    {
        public string ResourceId { get; set; }
        public decimal Cost { get; set; }
        public decimal RecommendCost { get; set; }
        public string Size { get; set; }
        public string RecommendSize { get; set; }
    }
}
