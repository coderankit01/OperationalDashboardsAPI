using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
   public interface IEC2Repository
    {
        string Region { get; set; }
        Task<DescribeInstancesResponse> GetInstanceDetails(DescribeInstancesRequest instancesRequest);
        Task<DescribeInstancesResponse> GetInstances();
        Task<DescribeRegionsResponse> GetRegions();
    }
}
