using Amazon.CloudWatch.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
    public interface ICloudWatchRepository
    {
        string Region { get; set; }
        Task<ListMetricsResponse> ListMetrics(ListMetricsRequest listMetricsRequest);
        Task<ListMetricsResponse> ListMetrics();
        Task<GetMetricDataResponse> GetMetricData(GetMetricDataRequest metricDataRequest);
        Task<GetMetricStatisticsResponse> GetMetricStatistics(GetMetricStatisticsRequest metricStatisticsRequest);
    }
}
