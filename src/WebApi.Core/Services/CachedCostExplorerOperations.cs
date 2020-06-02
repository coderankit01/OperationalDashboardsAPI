using Amazon.CostExplorer.Model;
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
    public class CachedCostExplorerOperations:ICostExplorerOperations
    {
        private static CostExplorerOperations costExplorerOperations { get; set; }
        private static IMemoryCache cache { get; set; }
        public CachedCostExplorerOperations(IMemoryCache _cache, CostExplorerOperations _costExplorerOperations)
        {
            costExplorerOperations = _costExplorerOperations;
            cache = _cache;
        }
        public async Task<List<CostUsageResponse>> GetCostUsage(CostUsageRequest costUsageRequest)
        {
            var cacheKey = CacheHelpers.GenerateServiceCacheItemKey(costUsageRequest);

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await costExplorerOperations.GetCostUsage(costUsageRequest);
            });
        }
        public async Task<List<CostUsageResponse>> GetCurrentYearCost(CostUsageRequest costUsageRequest)
        {
            var cacheKey = CacheHelpers.GenerateCacheItemKeyForYear(costUsageRequest);

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await costExplorerOperations.GetCurrentYearCost(costUsageRequest);
            });
        }
        public async Task<List<CostUsageResponse>> GetCurrentMonthCost(CostUsageRequest costUsageRequest)
        {
            var cacheKey = CacheHelpers.GenerateCacheItemKeyForMonth(costUsageRequest);

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await costExplorerOperations.GetCurrentMonthCost(costUsageRequest);
            });
        }
        public async Task<List<CostUsageResponse>> GetCostByMonth(CostUsageRequest costUsageRequest)
        {
            var cacheKey = CacheHelpers.GenerateCostByMonthCacheItemKey(costUsageRequest);

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await costExplorerOperations.GetCostByMonth(costUsageRequest);
            });
        }

        public async Task<List<CostUsageResponse>> GetCostForecast(CostUsageRequest costUsageRequest)
        {
           
            var response = await costExplorerOperations.GetCostForecast(costUsageRequest);
            return response;
        }
    }
}
