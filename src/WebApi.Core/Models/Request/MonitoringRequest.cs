﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Request
{
    public class MonitoringRequest
    {
        public string NameSpace { get; set; }
        public string Metrics { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool IsDateCustom { get; set; }
        public int RelativeMinutes { get; set; }
        public string NextToken { get; set; }
    }
}
