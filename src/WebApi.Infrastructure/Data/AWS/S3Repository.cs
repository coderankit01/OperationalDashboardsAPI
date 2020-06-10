using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using OperationalDashboard.Web.Api.Infrastructure.Base;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
   public class S3Repository: AWSBaseClient,IS3Repository
    {
        public string Region { get; set; }
        public async Task<ListBucketsResponse> GetS3Details()
        {
            using (var s3Client = new AmazonS3Client(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await s3Client.ListBucketsAsync();
                return response;
            }
                
        }
        public async Task<ListBucketsResponse> GetS3Details(ListBucketsRequest listBucketsRequest)
        {
            using (var s3Client = new AmazonS3Client(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await s3Client.ListBucketsAsync(listBucketsRequest);
                return response;
            }
        }
    }
}
