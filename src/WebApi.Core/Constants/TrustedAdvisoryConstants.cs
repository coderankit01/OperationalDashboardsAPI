using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Constants
{
   public static class TrustedAdvisoryConstants
    {
        public static readonly List<string> AdvisoryCategory = new List<string>()
        {
            "performance",
            "cost_optimizing",
            "security",
            "fault_tolerance",
            "service_limits"
        };
    }
}
