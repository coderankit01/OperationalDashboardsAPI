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
   public class EBSRepository: AWSBaseClient,IEBSRepository
    {
        public string Region { get; set; }
        public async Task<DescribeVolumesResponse> GetEBS()
        {
            using (var eBSClient = new AmazonEC2Client(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await eBSClient.DescribeVolumesAsync();
                return response;
            }
               
        }
        public async Task<DescribeVolumesResponse> GetEBS(DescribeVolumesRequest describeVolumesRequest)
        {
            using (var eBSClient = new AmazonEC2Client(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await eBSClient.DescribeVolumesAsync(describeVolumesRequest);
                return response;
            }
        }
    }
}
