using Amazon.RDS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
   public interface IRDSRepostitory
    {
        string Region { get; set; }
        Task<DescribeDBInstancesResponse> GetDatabaseDetails(DescribeDBInstancesRequest describeDBInstancesRequest);
        Task<DescribeDBInstancesResponse> GetDatabases();
    }
}
