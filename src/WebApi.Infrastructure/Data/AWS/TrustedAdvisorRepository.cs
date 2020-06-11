using System;
using System.Collections.Generic;
using System.Text;
using Amazon.AWSSupport;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon;
using Amazon.AWSSupport.Model;
using Amazon.RDS.Model;
using Amazon.S3;
using OperationalDashboard.Web.Api.Infrastructure.Base;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
   public  class TrustedAdvisorRepository : AWSBaseClient, ITrustedAdvisorRepository
    {
        public string Region { get; set; }
        public async Task<DescribeTrustedAdvisorChecksResponse> GetTrustedAdvisorChecks(DescribeTrustedAdvisorChecksRequest describeTrustedAdvisorChecksRequest)
        {
            using(var  amazonAWSSupportClient= new AmazonAWSSupportClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonAWSSupportClient.DescribeTrustedAdvisorChecksAsync(describeTrustedAdvisorChecksRequest);
                return response;    
            }
        }
        //checkresult
        public async Task<DescribeTrustedAdvisorCheckResultResponse> GetTrustedAdvisorCheckResult(DescribeTrustedAdvisorCheckResultRequest describeTrustedAdvisorCheckResultRequest)
        {
            using(var amazonAWSSupportClient = new AmazonAWSSupportClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonAWSSupportClient.DescribeTrustedAdvisorCheckResultAsync(describeTrustedAdvisorCheckResultRequest);
                return response;
            }
        }
        //CheckSummary
        public async Task<DescribeTrustedAdvisorCheckSummariesResponse> GetTrustedAdvisorCheckSummary(DescribeTrustedAdvisorCheckSummariesRequest describeTrustedAdvisorCheckSummariesRequest)
        {
            using (var amazonAWSSupportClient = new AmazonAWSSupportClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonAWSSupportClient.DescribeTrustedAdvisorCheckSummariesAsync(describeTrustedAdvisorCheckSummariesRequest);
                return response;
            }
        }
        //amke operations in Services
        //include in Startup class

    }
}
