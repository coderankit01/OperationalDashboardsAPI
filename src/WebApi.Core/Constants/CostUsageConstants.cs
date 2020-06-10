using OperationalDashboard.Web.Api.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Constants
{
   public static class CostUsageConstants
    {
        public static readonly List<string> granularity = new List<string>
        {
            "MONTHLY",
            "DAILY"
        };
        public static readonly List<string> GroupBy = new List<string>
        {
          "AZ",
            "INSTANCE_TYPE",
            "LEGAL_ENTITY_NAME",
            "LINKED_ACCOUNT",
            "OPERATION",
            "PLATFORM",
            "PURCHASE_TYPE",
            "SERVICE",
            "TAGS",
            "TENANCY",
            "RECORD_TYPE",
            "USAGE_TYPE"
        };
        public static readonly List<string> Metrics = new List<string>
        {
            "AmortizedCost",
            "BlendedCost",
            "NetAmortizedCost",
            "NetUnblendedCost",
            "NormalizedUsageAmount", 
            "UnblendedCost",
            "UsageQuantity"
        };
        public static readonly List<string> Filter = new List<string>
        {
                "LINKED_ACCOUNT"
            
            

        };

    }
}
