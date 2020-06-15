using Amazon.AWSSupport.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
   public  class StateCountResponse
    {
        public  int OkCount { get; set; }
        public  int WarningCount { get; set; }
        public  int ErrorCount { get; set; }
        public  int NotAvailableCount { get; set; }
 
    }
}
