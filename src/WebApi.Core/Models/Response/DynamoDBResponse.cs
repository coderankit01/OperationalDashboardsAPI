using System;
using System.Collections.Generic;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Models.Response
{
   public class DynamoDBResponse
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string PartitionKey { get; set; }
        public string SortKey { get; set; }
        public string Indexes { get; set; }
        public string TableSize { get; set; }
        public string CreatedDate { get; set; }
       
    }
}
