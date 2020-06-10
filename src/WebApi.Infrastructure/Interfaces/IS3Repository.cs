using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
    public interface IS3Repository
    {
        string Region { get; set; }
        Task<ListBucketsResponse> GetS3Details();
        Task<ListBucketsResponse> GetS3Details(ListBucketsRequest listBucketsRequest);
    }
}
