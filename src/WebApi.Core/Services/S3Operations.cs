using Amazon.S3.Model;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Core.Models.Response;
using OperationalDashboard.Web.Api.Infrastructure.Data.AWS;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
  public  class S3Operations : IResourceDetails
    {
        private static IS3Repository s3Repository { get; set; }
        public S3Operations(IS3Repository _s3Repository)
        {
            s3Repository = _s3Repository;
        }
        public S3Operations()
        {

        }

        public async Task<object> GetResourceDetails(MonitoringResourceRequest monitoringResourceRequest)
        {
            s3Repository = new S3Repository();
            s3Repository.Region = monitoringResourceRequest.Region;
            var response = await s3Repository.GetS3Details();
            return response;
        }

        public async Task<MonitoringSummaryResponse> GetResources(string region)
        {
            s3Repository = new S3Repository();
            s3Repository.Region = region;
            var response = await s3Repository.GetS3Details();
            var resources = response.Buckets.Select(x => x.BucketName).ToList();
            return new MonitoringSummaryResponse()
            {
                Label = "AWS/S3",
                Count = resources.Count,
                ResourcesId = resources
            };
        }
    }
}
