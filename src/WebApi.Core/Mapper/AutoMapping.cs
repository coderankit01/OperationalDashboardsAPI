using Amazon.CloudWatch.Model;
using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;
using AutoMapper;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dimension = Amazon.CostExplorer.Dimension;

namespace OperationalDashboard.Web.Api.Core.Mapper
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
        
            CreateMap<GroupBy, GroupDefinition>()
                .ForMember(d => d.Type, opt => opt.MapFrom(s => GroupDefinitionType.FindValue(s.Type)));
            CreateMap<CostUsageRequest, GetCostAndUsageRequest>()
               .ForMember(d => d.Granularity, opt => opt.MapFrom(s => Granularity.FindValue(s.Granularity)))
               .ForMember(d => d.TimePeriod, opt => opt.MapFrom(s => new DateInterval() { Start = s.StartDate.ToString("yyyy-MM-dd"), End = s.EndDate.ToString("yyyy-MM-dd") }))
               .ForMember(d => d.Metrics, opt => opt.MapFrom(s =>new List<string>() { s.Metrics }))
               .ForMember(d => d.GroupBy, opt => opt.MapFrom(s => s.GroupBy))
               .ForMember(d => d.Filter, opt => opt.MapFrom(s => s.Filters.Values.Any()? new Expression() { Dimensions = new DimensionValues() { Key = Dimension.FindValue(s.Filters.Key), Values = s.Filters.Values } }:null));

            CreateMap<CostUsageRequest, GetCostForecastRequest>()
                  .ForMember(d => d.Filter, opt => opt.MapFrom(s => s.Filters.Values.Any() ? new Expression() { Dimensions = new DimensionValues() { Key = Dimension.FindValue(s.Filters.Key), Values = s.Filters.Values } } : null));

            CreateMap<MonitoringResponse, MetricDataResult>()
                  .ForMember(d => d.StatusCode, opt => opt.Ignore())
                  .ForMember(d => d.Id, opt => opt.Ignore())
                  .ForMember(d => d.Messages, opt => opt.Ignore());

            CreateMap<MetricDataResult, MonitoritingMetrics>();
        }
    }
}
