using Amazon.CostExplorer.Model;
using Microsoft.Extensions.Caching.Memory;
using OperationalDashboard.Web.Api.Core.Extensions;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
    public class CachedCostRecommendationsOperations: ICostRecommendationsOperations
    {
        private static CostRecommendationsOperations costRecommendationsOperations { get; set; }
        private static IMemoryCache cache { get; set; }
        public CachedCostRecommendationsOperations(IMemoryCache _cache,CostRecommendationsOperations _costRecommendationsOperations)
        {
            costRecommendationsOperations = _costRecommendationsOperations;
            cache = _cache;
        }

        public async Task<List<CostRecommendationResponse>> GetCostSummaryDetails()
        {
            var cacheKey = CacheHelpers.GenerateCacheKeyForCostRecommendation();

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await costRecommendationsOperations.GetCostSummaryDetails();
            });
            
        }

        public async Task<CostRecommendationSummaryResponse> GetCostSummary()
        {
            var cacheKey = CacheHelpers.GenerateCacheKeyForCostRecommnedationSummary();

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await costRecommendationsOperations.GetCostSummary();
            });
        }

        public async Task<object> GetCpuVsRecommendCpuUsage()
        {
            var cacheKey = CacheHelpers.GenerateCacheKeyForCostRecommendationCpu();

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await costRecommendationsOperations.GetCpuVsRecommendCpuUsage();
            });
        }

        public async Task<object> GetUsedVsUnusedCpu()
        {
            var cacheKey = CacheHelpers.GenerateCacheKeyForCostRecommnedationUnsedCpu();

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await costRecommendationsOperations.GetUsedVsUnusedCpu();
            });
        }

        public async Task<object> GetCostVsSavings()
        {
            var cacheKey = CacheHelpers.GenerateCacheKeyForCostRecommendationSaving();

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheHelpers.absoluteExpirationRelativeToNow;
                return await costRecommendationsOperations.GetCostVsSavings();
            });
        }

        public Task<GetRightsizingRecommendationResponse> GetHighRecommActivities()
        {
            throw new NotImplementedException();
        }
    }
}
