using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
  public  class LambdaResponse
    {
        public string FunctionName { get; set; }
        public string Description { get; set; }
        public string Runtime { get; set; }
        public string CodeSize { get; set; }
        public string LastModified { get; set; }
    }
}
