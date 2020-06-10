using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.RDS;
using Amazon.RDS.Model;
using OperationalDashboard.Web.Api.Infrastructure.Base;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
   public class RDSRepostitory: AWSBaseClient,IRDSRepostitory
    {
        public string Region { get; set; }
        public async Task<DescribeDBInstancesResponse> GetDatabaseDetails(DescribeDBInstancesRequest describeDBInstancesRequest)
        {
            using (var amazonRDSClient = new AmazonRDSClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonRDSClient.DescribeDBInstancesAsync(describeDBInstancesRequest);
                return response;
            }
        }
                
        public async Task<DescribeDBInstancesResponse> GetDatabases()
        {
            using (var amazonRDSClient = new AmazonRDSClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonRDSClient.DescribeDBInstancesAsync();
                return response;

            }
        }

    }
}
