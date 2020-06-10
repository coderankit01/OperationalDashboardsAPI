using Amazon.EC2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
  public  interface IEBSRepository
    {
        string Region { get; set; }
        Task<DescribeVolumesResponse> GetEBS(DescribeVolumesRequest describeVolumesRequest);
        Task<DescribeVolumesResponse> GetEBS();
    }
}
