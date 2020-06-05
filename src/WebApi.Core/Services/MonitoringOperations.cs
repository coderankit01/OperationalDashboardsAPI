﻿using Amazon.CloudWatch.Model;
using Amazon.CostExplorer.Model;
using AutoMapper;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
   public class MonitoringOperations: IMonitoringOperations
    {

        private static IMapper mapper { get; set; }
        private static ICloudWatchRepository cloudWatchRepository { get; set; }
        public MonitoringOperations(IMapper _mapper, ICloudWatchRepository _cloudWatchRepository)
        {
            mapper = _mapper;
            cloudWatchRepository = _cloudWatchRepository;
        }
        public async Task<ListMetricsResponse> GetAllMetrics()
        {
            return await cloudWatchRepository.ListMetrics();
        }
        private GetMetricDataRequest GenerateMetricRequest(List<Metric> metrics,DateTime startDate,DateTime endDate,string nextToken)
        {
            var metricQueryCollection = new List<MetricDataQuery>();
            int counter = 0;
            var period = (int)(Math.Ceiling((endDate - startDate).TotalSeconds / 12) / 60) * 60;
            foreach (var metric in metrics)
            {
                metricQueryCollection.Add(new MetricDataQuery()
                {
                    Id = "m" + counter.ToString(),
                    MetricStat = new MetricStat()
                    {
                        Stat = "Average",
                        Period = period,
                        Metric = metric
                    }
                });
                counter++;
            }
            return new GetMetricDataRequest()
            {
                NextToken=nextToken,
                StartTimeUtc = startDate,
                EndTimeUtc = endDate,
                MetricDataQueries = metricQueryCollection
            };

        }
         public async Task<MonitoringResponse> GetMetricsData(MonitoringRequest monitoringRequest, List<Metric> metrics)
        {
            
            var startDate = DateTime.Now.AddMinutes((-1) * monitoringRequest.RelativeMinutes);
            var endDate = DateTime.Now;
            if (monitoringRequest.IsDateCustom)
            {
                startDate = monitoringRequest.StartDateTime.Value;
                endDate = monitoringRequest.EndDateTime.Value;
            }
            var metricDataRequest = GenerateMetricRequest(metrics,startDate,endDate,monitoringRequest.NextToken);
            var response = await cloudWatchRepository.GetMetricData(metricDataRequest);
            var mapResponse = mapper.Map<List<MonitoritingMetrics>>(response.MetricDataResults);
            return new MonitoringResponse() {NextToken=response.NextToken,MetricResponse=mapResponse };
        }
        public async Task<ListMetricsResponse> GetMetrics()
        {
            var response = await cloudWatchRepository.ListMetrics();
            return response;

        }
        public async Task<List<Metric>> GetMetrics(string nameSpace,string metric)
        {
            var request = new ListMetricsRequest() { Namespace = nameSpace, MetricName = metric };
            var response = new ListMetricsResponse();
            do
            {
                response = await cloudWatchRepository.ListMetrics(request);
                request.NextToken = response.NextToken;
            } while (!string.IsNullOrEmpty(request.NextToken));

            return response.Metrics;

        }
        private async Task<GetMetricStatisticsResponse> GetMetricStatistics()
        {
            var metricsResponse = await cloudWatchRepository.ListMetrics(new ListMetricsRequest() { Namespace = "AWS/EC2" });
            var filterResponse = metricsResponse.Metrics.Where(x => x.Dimensions.Any(y => (y.Value == "i-0adbe578a746a6bd7" || y.Value == "i-0ee9ec3833e6f59f7")));
            //metricsResponse.Metrics.SelectMany(x => x.Dimensions).Distinct().Take(5).ToList()

            var metericRequest = new GetMetricStatisticsRequest()
            {
                StartTimeUtc = DateTime.Now.AddDays(-1),
                EndTimeUtc = DateTime.Now,
                Namespace = "AWS/EC2",
                MetricName = filterResponse.FirstOrDefault().MetricName,
                Period = 300,
                Statistics = new List<string>() { "Minimum", "Maximum", "Average", "SampleCount", "Sum" },//Minimum, Maximum, Average, SampleCount, Sum
                Dimensions = filterResponse.SelectMany(x=>x.Dimensions).GroupBy(y=>new { y.Name, y.Value }).Select(z=>new Dimension() { Name=z.Key.Name,Value= z.Key.Value}).ToList()

            };
            var response = await cloudWatchRepository.GetMetricStatistics(metericRequest);
            return response;
        }
    }
}