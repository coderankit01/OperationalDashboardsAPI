using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Request
{
   public class AWSProfileCredentials
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string ProfileName { get; set; }
        public string UserToken { get; set; }
    }
}
