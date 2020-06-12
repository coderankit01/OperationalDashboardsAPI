using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
   public class Route53Response
    {
        public string DomainName { get; set; }
        public string Type { get; set; }
        public string RecordSetCount { get; set; }
        public string Comment { get; set; }
        public string HostedZoneID { get; set; }
    }
}
