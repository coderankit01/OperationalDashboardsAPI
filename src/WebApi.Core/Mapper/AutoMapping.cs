using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;
using AutoMapper;
using OperationalDashboard.Web.Api.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
               .ForMember(d => d.Filter, opt => opt.MapFrom(s => s.Filters.Values.Any()? new Expression() { Dimensions = new DimensionValues() { Key = Dimension.FindValue(s.Filters.Key), Values = s.Filters.Values } }:new Expression()));
        }
    }
}
