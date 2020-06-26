using Amazon.CloudWatch.Model;
using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;
using Amazon.EC2.Model;
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

            CreateMap<MetricDataResult, MonitoritingMetrics>()
                  .ForMember(d => d.Label, opt => opt.MapFrom(s => string.Join(" ", s.Label.Split(' ').Distinct())));

            CreateMap<Instance, Ec2Response>()
                .ForMember(d => d.InstanceID, opt => opt.MapFrom(s => s.InstanceId))
                .ForMember(d => d.InstanceType, opt => opt.MapFrom(s => s.InstanceType.Value))
                .ForMember(d => d.InstanceState, opt => opt.MapFrom(s => s.State.Name))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.KeyName))
                .ForMember(d => d.LaunchTime, opt => opt.MapFrom(s => s.LaunchTime.ToString()))
                .ForMember(d => d.SecurityGroup, opt => opt.MapFrom(s => String.Join(",", s.SecurityGroups.Select(x => x.GroupName))))
                .ForMember(d => d.Platform, opt => opt.MapFrom(s => s.Platform.Value));
        }
    }
}
