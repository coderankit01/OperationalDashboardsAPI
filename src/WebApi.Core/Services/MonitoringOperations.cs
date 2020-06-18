using Amazon.CloudWatch.Model;
using Amazon.CostExplorer.Model;
using Amazon.S3.Model.Internal.MarshallTransformations;
using AutoMapper;
using OperationalDashboard.Web.Api.Core.Constants;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Core.Models.Response;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
        public async Task<ListMetricsResponse> GetAllMetrics(string region)
        {
            cloudWatchRepository.Region = region;
            return await cloudWatchRepository.ListMetrics();
        }
        private GetMetricDataRequest GenerateMetricRequest(MonitoringRequest monitoringRequest, List<Metric> metrics)
        {
            var startDate = DateTime.Now.AddMinutes((-1) * monitoringRequest.RelativeMinutes);
            var endDate = DateTime.Now;
            if (monitoringRequest.IsDateCustom)
            {
                startDate = monitoringRequest.StartDateTime.Value;
                endDate = monitoringRequest.EndDateTime.Value;
            }
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
                NextToken= monitoringRequest.NextToken,
                StartTimeUtc = startDate,
                EndTimeUtc = endDate,
                MetricDataQueries = metricQueryCollection
            };

        }
         public async Task<MonitoringResponse> GetMetricsData(MonitoringRequest monitoringRequest, List<Metric> metrics)
        {
            cloudWatchRepository.Region = monitoringRequest.Region;
             var metricDataRequest = GenerateMetricRequest( monitoringRequest, metrics);
            if (!metricDataRequest.MetricDataQueries.Any())
            {
                return new MonitoringResponse() { 
                MetricResponse = new List<MonitoritingMetrics>()
                };
            }
            var response = await cloudWatchRepository.GetMetricData(metricDataRequest);
            var mapResponse = mapper.Map<List<MonitoritingMetrics>>(response.MetricDataResults);

            return new MonitoringResponse() {NextToken=response.NextToken,MetricResponse=mapResponse };
        }
        public  object MapResponse(List<MonitoritingMetrics> monitoritingMetrics,string metricType,int? limit)
        {
            switch (metricType)
            {
                case "LINE":
                    return LineResponse(monitoritingMetrics,limit);
                case "BAR":
                    return BarResponse(monitoritingMetrics,limit);
                case "PIE":
                    return PieResponse(monitoritingMetrics,limit);
                default:
                    return new { Name = "NA", Value = 0 };
            }
           
        }
        private object BarResponse(List<MonitoritingMetrics> monitoritingMetrics,int? limit)
        {
            if(limit.HasValue && limit != -1)
            {
                monitoritingMetrics.FirstOrDefault().Timestamps.Select((k, i) => new { Name = k.ToString(), Value = Math.Round(monitoritingMetrics.FirstOrDefault().Values[i], 2) }).OrderByDescending(o => o.Value).Take(limit.Value);
            }
            return monitoritingMetrics.FirstOrDefault().Timestamps.Select((k, i) => new { Name=k.ToString(), Value = Math.Round( monitoritingMetrics.FirstOrDefault().Values[i],2 )});
        }
        private object PieResponse(List<MonitoritingMetrics> monitoritingMetrics, int? limit)
        {
            if (limit.HasValue && limit != -1)
            {
                return monitoritingMetrics.Select(x => new { Name = x.Label, Value = Math.Round(x.Values.Sum(), 2) }).OrderByDescending(o => o.Value).Take(limit.Value);
            }
                return monitoritingMetrics.Select(x => new { Name = x.Label, Value = Math.Round(x.Values.Sum(),2) }).OrderByDescending(o=>o.Value);
        }
        private object LineResponse(List<MonitoritingMetrics> monitoritingMetrics, int? limit)
        {
            string variableName = "val";
            int count = 0;
            List<object> summaryObject = new List<object>();
            List<Dictionary<string, double>> mapResponses = new List<Dictionary<string, double>>();
            if (limit.HasValue && limit != -1)
            {
                monitoritingMetrics= monitoritingMetrics.Take(limit.Value).ToList();
            }
                foreach (var monitor in monitoritingMetrics)
            {
                summaryObject.Add(new { Name = monitor.Label, Value = variableName + count.ToString() });
                var mapresponse = monitor.Timestamps.Select((k, i) => new { k, v = monitor.Values[i] })
              .ToDictionary(x => x.k.ToString() + "~" + variableName + count.ToString(), x => x.v);

                mapResponses.Add(mapresponse);
                count++;
            }

            var timestamps = monitoritingMetrics.SelectMany(x => x.Timestamps.Select(y => y.ToString())).GroupBy(x => x).Select(t => t.Key.ToString());
            var expandObjs = new List<IDictionary<string, Object>>();
            foreach (var timestamp in timestamps.OrderByDescending(x => Convert.ToDateTime(x)))
            {
                var splitObjects = mapResponses.SelectMany(y => y).Where(x => x.Key.Split('~')[0] == timestamp);
                var expandObj = new ExpandoObject() as IDictionary<string, Object>;
                expandObj.Add("timestamp", timestamp);
                foreach (var _object in splitObjects)
                {
                    expandObj.Add(_object.Key.Split('~')[1], Math.Round(_object.Value,2));

                }
                expandObjs.Add(expandObj);
            }
            return new { result = expandObjs, summary = summaryObject };
        } 
        public async Task<ListMetricsResponse> GetMetrics(string region)
        {
            cloudWatchRepository.Region = region;
            var response = await cloudWatchRepository.ListMetrics();
            return response;

        }
        public async Task<MonitoringSummaryResponse> GetResourceSummary(string region,string nameSpace)
        {
            var metrics = await GetMetrics(region,nameSpace);
            var nameSpaceIdentifier = MonitoringConstants.nameSpaceIdentifiers.ContainsKey(nameSpace)? MonitoringConstants.nameSpaceIdentifiers[nameSpace]:string.Empty;
            if (string.IsNullOrEmpty(nameSpaceIdentifier))
            {
                return new MonitoringSummaryResponse() { Label = nameSpace, Count = 0 };
            }
            var resources = metrics.SelectMany(x => x.Dimensions).Where(y => y.Name.Equals(nameSpaceIdentifier)).GroupBy(z=>new { z.Value}).Select(key=>key.Key.Value).ToList();
            return new MonitoringSummaryResponse()
            {
                Label = nameSpace,
                Count = resources.Count,
                ResourcesId = resources
            };
        }
        public async Task<List<Metric>> GetMetrics(string region,string nameSpace)
        {
            cloudWatchRepository.Region = region;
            var request = new ListMetricsRequest() { Namespace = nameSpace, };
            var response = new ListMetricsResponse();
            do
            {
                response = await cloudWatchRepository.ListMetrics(request);
                request.NextToken = response.NextToken;
            } while (!string.IsNullOrEmpty(request.NextToken));

            return response.Metrics;

        }
        public async Task<List<Metric>> GetMetrics(string region, string nameSpace,string metric)
        {
            cloudWatchRepository.Region = region;
            var request = new ListMetricsRequest() { Namespace = nameSpace, MetricName = metric };
            var response = new ListMetricsResponse();
            do
            {
                response = await cloudWatchRepository.ListMetrics(request);
                request.NextToken = response.NextToken;
            } while (!string.IsNullOrEmpty(request.NextToken));

            return response.Metrics;

        }
        public async Task<List<Metric>> GetMetrics(string region, string nameSpace, string metric,string dimension)
        {
            cloudWatchRepository.Region = region;
            var dimensionFilter = new List<DimensionFilter>() { new DimensionFilter() { 
                                   Name=MonitoringConstants.nameSpaceIdentifiers[nameSpace],
                                   Value=dimension
            } };
            var request = new ListMetricsRequest() { Namespace = nameSpace, MetricName = metric,Dimensions= dimensionFilter };
            var response = new ListMetricsResponse();
            do
            {
                response = await cloudWatchRepository.ListMetrics(request);
                request.NextToken = response.NextToken;
            } while (!string.IsNullOrEmpty(request.NextToken));

            return response.Metrics;

        }
        public async Task<List<Metric>> GetMetrics(string region, string nameSpace, string metric, List<string> dimensions)
        {
            cloudWatchRepository.Region = region;
            var dimensionFilter = new List<DimensionFilter>();
            dimensions.ForEach(x => {
                dimensionFilter.Add(new DimensionFilter()
                {
                    Name = MonitoringConstants.nameSpaceIdentifiers[nameSpace],
                    Value = x
                });
            });
            var request = new ListMetricsRequest() { Namespace = nameSpace, MetricName = metric, Dimensions = dimensionFilter };
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
