using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
   public class ResourceRecommendationResponse
    {
        public string CheckId { get; set; }
        public string CheckName { get; set; }
        public string Recommendation { get; set; }
        public string Status { get; set; }
        public int ResourceCount { get; set; }




    }
}
