using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
   public class Ec2Response
    {
        public string Name { get; set; }
        public string InstanceID { get; set; }
        public string InstanceType { get; set; }
        public string InstanceState { get; set; }
        public string LaunchTime { get; set; }
        public string SecurityGroup { get; set; }
        public string Platform { get; set; }
    }
}
