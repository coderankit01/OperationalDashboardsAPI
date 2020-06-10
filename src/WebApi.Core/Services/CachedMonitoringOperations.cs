using Amazon.CloudWatch.Model;
using Microsoft.Extensions.Caching.Memory;
using OperationalDashboard.Web.Api.Core.Extensions;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
    public class CachedMonitoringOperations : IMonitoringOperations
    {

        private static MonitoringOperations monitortingOperations { get; set; }
        private static IMemoryCache cache { get; set; }
        public CachedMonitoringOperations(IMemoryCache _cache, MonitoringOperations _monitoringOperations)
        {
            monitortingOperations = _monitoringOperations;
            cache = _cache;
        }
        public async Task<MonitoringResponse> GetMetricsData(MonitoringRequest monitoringRequest, List<Metric> metrics)
        {
            var response = await monitortingOperations.GetMetricsData(monitoringRequest, metrics);
            return response;
        }
        public async Task<ListMetricsResponse> GetMetrics(string region)
        {
            var cacheKey = CacheHelpers.GenerateCacheKeyForListMetric();

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await monitortingOperations.GetMetrics(region);
            });
        }
        public async Task<List<Metric>> GetMetrics(string region, string nameSpace, string metric)
        {
            var cacheKey = CacheHelpers.GenerateCacheKeyForMetric(nameSpace, metric);

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await monitortingOperations.GetMetrics(region, nameSpace, metric);
            });
        }
        public async Task<MonitoringSummaryResponse> GetResourceSummary(string region, string nameSpace)
        {
            return await monitortingOperations.GetResourceSummary(region, nameSpace);
        }
        public async Task<List<Metric>> GetMetrics(string region, string nameSpace, string metric,string dimension)
        {
            return await monitortingOperations.GetMetrics(region, nameSpace, metric, dimension);
        }
    }
}
