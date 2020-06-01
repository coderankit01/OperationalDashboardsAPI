using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
    public class CostUsageResponse
    {
        public string Name { get; set; }
        public decimal Amount  { get; set; }
        public long Count { get; set; }
        public DateTime Date { get; set; }
    }
}
