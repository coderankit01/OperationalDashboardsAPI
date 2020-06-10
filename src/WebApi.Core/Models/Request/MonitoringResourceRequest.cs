using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Request
{
   public class MonitoringResourceRequest
    {
        public string Namespace { get; set; }
        public List<string> ResourceIds { get; set; }
        public string Region { get; set; }
    }
}
