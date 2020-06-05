using Amazon;
using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
   public class CloudWatchRepository: ICloudWatchRepository
    {
        AmazonCloudWatchClient cloudWatchClient = new AmazonCloudWatchClient("AKIAV4RXYT3ZMYJB3RCD", "mdbi7G03FqEjJWjC1VJHNuT+GkE5d5Qtf2CxbDTW", RegionEndpoint.GetBySystemName("us-east-1"));
        
        public async Task<ListMetricsResponse> ListMetrics(ListMetricsRequest listMetricsRequest)
        {
            var response = await cloudWatchClient.ListMetricsAsync(listMetricsRequest);
            return response;
        }
        public async Task<ListMetricsResponse> ListMetrics()
        {
            var response = await cloudWatchClient.ListMetricsAsync();
            return response;
        }
        public async Task<GetMetricDataResponse> GetMetricData(GetMetricDataRequest metricDataRequest)
        {
            var response = await cloudWatchClient.GetMetricDataAsync(metricDataRequest);
            return response;

        }
        public async Task<GetMetricStatisticsResponse> GetMetricStatistics(GetMetricStatisticsRequest metricStatisticsRequest)
        {
            var response = await cloudWatchClient.GetMetricStatisticsAsync(metricStatisticsRequest);
            return response;

        }

    }
}
