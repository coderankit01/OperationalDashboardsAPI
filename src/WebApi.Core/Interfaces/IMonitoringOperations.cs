using Amazon.CloudWatch.Model;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Interfaces
{
   public interface IMonitoringOperations
    {
        Task<MonitoringResponse> GetMetricsData(MonitoringRequest monitoringRequest, List<Metric> metrics);
        Task<List<Metric>> GetMetrics(string region,string nameSpace, string metric);
        Task<List<Metric>> GetMetrics(string region, string nameSpace, string metric, string dimension);
        Task<List<Metric>> GetMetrics(string region, string nameSpace, string metric, List<string> dimensions);
        Task<ListMetricsResponse> GetMetrics(string region);
        Task<MonitoringSummaryResponse> GetResourceSummary(string region, string nameSpace);
        object MapResponse(List<MonitoritingMetrics> monitoritingMetrics, string metricType,int? limit);

    }
}
