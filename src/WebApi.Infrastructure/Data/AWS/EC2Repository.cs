using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using OperationalDashboard.Web.Api.Infrastructure.Base;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
  public class EC2Repository: AWSBaseClient, IEC2Repository 
    {
        // AmazonEC2Client amazonEC2Client = new AmazonEC2Client("AKIAV4RXYT3ZMYJB3RCD", "mdbi7G03FqEjJWjC1VJHNuT+GkE5d5Qtf2CxbDTW", RegionEndpoint.GetBySystemName("us-east-1"));
        public string Region { get; set; }
        public async Task<DescribeInstancesResponse> GetInstanceDetails(DescribeInstancesRequest instancesRequest)
        {
            using (AmazonEC2Client amazonEC2Client = new AmazonEC2Client(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonEC2Client.DescribeInstancesAsync(instancesRequest);
                return response;
            }
        }
        public async Task<DescribeInstancesResponse> GetInstances()
        {
            using (AmazonEC2Client amazonEC2Client = new AmazonEC2Client(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonEC2Client.DescribeInstancesAsync();
                return response;
            }
        }
        public async Task<DescribeRegionsResponse> GetRegions()
        {
            using (AmazonEC2Client amazonEC2Client = new AmazonEC2Client(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonEC2Client.DescribeRegionsAsync();
                return response;
            }
        }
    }
}
