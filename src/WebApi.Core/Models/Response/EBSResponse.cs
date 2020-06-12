using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
   public class EBSResponse
    {
        public string Name { get; set; }
        public string VolumeID { get; set; }
        public string Size { get; set; }
        public string VolumeType { get; set; }
        public string Snapshot { get; set; }
        public string CreatedDate { get; set; }
        public string VolumeState { get; set; }
    }
}
