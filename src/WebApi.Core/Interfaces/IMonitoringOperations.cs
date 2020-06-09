using Amazon.CloudWatch.Model;
using OperationalDashboard.Web.Api.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Interfaces
{
   public interface IMonitoringOperations
    {
        Task<MonitoringResponse> GetMetricsData(MonitoringRequest monitoringRequest, List<Metric> metrics);
        Task<List<Metric>> GetMetrics(string nameSpace, string metric);
        Task<ListMetricsResponse> GetMetrics();
        Task<MonitoringSummaryResponse> GetResourceSummary(string nameSpace);

    }
}
