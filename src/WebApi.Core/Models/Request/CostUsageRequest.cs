using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OperationalDashboard.Web.Api.Core.Models.Request
{
  public  class CostUsageRequest
    {
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public string Granularity { get; set; }
        public List<GroupBy> GroupBy { get; set; }
        public string Metrics { get; set; }
        public Filter Filters { get; set; }
        public int? Limit { get; set; } = 10;
    }
    public class GroupBy
    {
        public string Key { get; set; }
        public string Type { get; set; }
    }
    public class Filter
    {
        public string Key { get; set; }
        public List<string> Values { get; set; }
    }

}
