﻿using OperationalDashboard.Web.Api.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperationalDashboard.Web.Api.Core.Extensions
{
  public static  class CacheHelpers
    {
        private static readonly string _serviceItemsKeyTemplate = "service-{0}-{1}-{2}-{3}-{4}-{5}";
        private static readonly string _costByMonthItemsKeyTemplate = "costMonth-{0}-{1}-{2}-{3}-{4}-{5}";
        private static readonly string _yearItemsKeyTemplate = "year-{0}-{1}-{2}-{3}";
        private static readonly string _monthItemsKeyTemplate = "month-{0}-{1}-{2}-{3}";
        public static readonly TimeSpan absoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
        public static string GenerateServiceCacheItemKey(CostUsageRequest costUsageRequest)
        {

            return string.Format(_serviceItemsKeyTemplate, DateTime.Now.ToString("yyyyMMdd"), costUsageRequest.StartDate.ToString("yyyyMMdd"), costUsageRequest.EndDate.ToString("yyyyMMdd"), String.Join("-",costUsageRequest.GroupBy.Select(x=>x.Key)), costUsageRequest.Granularity, String.Join("-", costUsageRequest.Filters.Values));
        }
        public static string GenerateCostByMonthCacheItemKey(CostUsageRequest costUsageRequest)
        {

            return string.Format(_costByMonthItemsKeyTemplate, DateTime.Now.ToString("yyyyMMdd"), costUsageRequest.StartDate.ToString("yyyyMMdd"), costUsageRequest.EndDate.ToString("yyyyMMdd"), String.Join("-", costUsageRequest.GroupBy.Select(x => x.Key)), costUsageRequest.Granularity, String.Join("-", costUsageRequest.Filters.Values));
        }
        public static string GenerateCacheItemKeyForYear(CostUsageRequest costUsageRequest)
        {
            return string.Format(_yearItemsKeyTemplate, DateTime.Now.ToString("yyyyMMdd"), costUsageRequest.StartDate.ToString("yyyyMMdd"), costUsageRequest.EndDate.ToString("yyyyMMdd"), String.Join("-", costUsageRequest.Filters.Values));
        }
        public static string GenerateCacheItemKeyForMonth(CostUsageRequest costUsageRequest)
        {
            return string.Format(_monthItemsKeyTemplate, DateTime.Now.ToString("yyyyMMdd"), costUsageRequest.StartDate.ToString("yyyyMMdd"), costUsageRequest.EndDate.ToString("yyyyMMdd"), String.Join("-", costUsageRequest.Filters.Values));
        }
    }
}