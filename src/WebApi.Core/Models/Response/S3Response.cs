using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
   public class S3Response
    {
        public string BucketName { get; set; }
        public string CreatedDate { get; set; }
        public string Region { get; set; }
    }
}
