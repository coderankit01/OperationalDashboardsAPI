using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Request
{
   public class MonitoringResponse
    {
       public List<MonitoritingMetrics> MetricResponse { get; set; }
       public string NextToken { get; set; }
    }
    public class MonitoritingMetrics
    {
        public string Label { get; set; }
        public List<double> Values { get; set; }
        public List<DateTime> Timestamps { get; set; }
        
    }
    public class MonitoringSummaryResponse
    {
        public string Label { get; set; }
        public int Count { get; set; }
        public List<string> ResourcesId { get; set; }
    }
}
