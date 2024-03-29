﻿using Amazon.CloudWatch.Model;
using Amazon.CostExplorer;
using Amazon.CostExplorer.Model;
using AutoMapper;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Core.Models.Response;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
    public class CostExplorerOperations: ICostExplorerOperations
    {
        private static IMapper mapper { get; set; }
        private static ICostExplorerRepository costExplorerRepository { get; set; }
        public CostExplorerOperations(IMapper _mapper, ICostExplorerRepository _costExplorerRepository)
        {
            mapper = _mapper;
            costExplorerRepository = _costExplorerRepository;
        }
        public async Task<List<CostUsageResponse>> GetCostUsage(CostUsageRequest costUsageRequest)
        {
            var mapRequest = mapper.Map<GetCostAndUsageRequest>(costUsageRequest);
            var response = await costExplorerRepository.GetCostAndUsage(mapRequest);
            var mapResponse = response.ResultsByTime.SelectMany(x => x.Groups).GroupBy(x => x.Keys[response.GroupDefinitions.Count-1])
                              .Select(z => new CostUsageResponse 
                                      {
                                         Name = z.Key,
                                         Amount = z.Sum(c => Convert.ToDecimal(c.Metrics[costUsageRequest.Metrics].Amount))
                                      }
                                     );
            if (costUsageRequest.Limit.HasValue && costUsageRequest.Limit != -1)
            {
                return mapResponse.OrderByDescending(x => x.Amount).Take(costUsageRequest.Limit.Value).ToList();
            }
            return mapResponse.ToList();
        }
        public async Task<List<CostUsageResponse>> GetLinkedAccounts()
        {
            CostUsageRequest costUsageRequest = new CostUsageRequest()
            {
                StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now,
                Granularity = "MONTHLY",
                GroupBy = new List<GroupBy>()
                {
                    new GroupBy()
                {
                    Key = "LINKED_ACCOUNT",
                    Type = "DIMENSION"

                }
                },
                Metrics="UnblendedCost"
            };
            var mapRequest = mapper.Map<GetCostAndUsageRequest>(costUsageRequest);
            var response = await costExplorerRepository.GetCostAndUsage(mapRequest);
            var mapResponse = response.ResultsByTime.SelectMany(x => x.Groups).GroupBy(x => x.Keys[response.GroupDefinitions.Count - 1])
                              .Select(z => new CostUsageResponse
                              {
                                  Name = z.Key,
                              }
                                     );
            return mapResponse.ToList();
        }
        public async Task<List<CostUsageResponse>> GetCostByMonth(CostUsageRequest costUsageRequest)
        {
            var mapRequest = mapper.Map<GetCostAndUsageRequest>(costUsageRequest);
            var response = await costExplorerRepository.GetCostAndUsage(mapRequest);
            var mapResponse = response.ResultsByTime.Select(x => new { x.Groups, x.TimePeriod }).Select(z => new CostUsageResponse()
            {
                Name = Convert.ToDateTime(z.TimePeriod.Start).ToString("MMMM") + "-" + Convert.ToDateTime(z.TimePeriod.Start).Year.ToString(),
                Amount = z.Groups.Sum(s => Convert.ToDecimal(s.Metrics[costUsageRequest.Metrics].Amount))

            });
            return mapResponse.ToList();
        }
        public async Task<List<CostUsageResponse>> GetCurrentYearCost(CostUsageRequest costUsageRequest)
        {
            costUsageRequest.StartDate = new DateTime(DateTime.Now.Year, 1, 1);
            costUsageRequest.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            costUsageRequest.Granularity = "MONTHLY";
            var mapRequest = mapper.Map<GetCostAndUsageRequest>(costUsageRequest);
            var response = await costExplorerRepository.GetCostAndUsage(mapRequest);
            var mapResponse = response.ResultsByTime.SelectMany(x => x.Groups).Select(amt => Convert.ToDecimal(amt.Metrics[costUsageRequest.Metrics].Amount)).Sum();
            return new List<CostUsageResponse>() { 
                         new CostUsageResponse()
                                  {
                                        Name = DateTime.Now.Year.ToString(),
                                        Amount = mapResponse
                                  }
            };
        }

        public async Task<List<CostUsageResponse>> GetCurrentMonthCost(CostUsageRequest costUsageRequest)
        {
            costUsageRequest.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            costUsageRequest.EndDate = costUsageRequest.StartDate.AddMonths(1).AddDays(-1);
            costUsageRequest.Granularity = "MONTHLY";
            var mapRequest = mapper.Map<GetCostAndUsageRequest>(costUsageRequest);
            var response = await costExplorerRepository.GetCostAndUsage(mapRequest);
            var totalDays = DateTime.DaysInMonth(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1).Year, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1).Month);
            var currentDay = DateTime.Now.Day;
            var lastMonthTotalCost = await GetLastMonthCost(costUsageRequest);
            var lastMonthsAvg= lastMonthTotalCost/ totalDays;
            var lastMonthAverageofDesiredDays = lastMonthsAvg * currentDay;
            var mapResponse = response.ResultsByTime.SelectMany(x => x.Groups).Select(amt => Convert.ToDecimal(amt.Metrics[costUsageRequest.Metrics].Amount)).Sum();
            var currentMonthPercentageChange = ((lastMonthAverageofDesiredDays - mapResponse) / mapResponse) * 100;
            return new List<CostUsageResponse>() {
                         new CostUsageResponse()
                                  {
                                        Name = DateTime.Now.ToString("MMMM")+ "-" + DateTime.Now.Year.ToString(),
                                        Amount = mapResponse,
                                        Custom= Convert.ToString(Math.Round(currentMonthPercentageChange,2))
                                  }
            };
        }
        //getForecastForCurrentMonth 
        public async Task<List<CostUsageResponse>> GetCostForecast(CostUsageRequest costUsageRequest)
        {
            try
            {
                var mapRequest = mapper.Map<GetCostForecastRequest>(costUsageRequest);
                mapRequest.Granularity = Granularity.MONTHLY;
                mapRequest.Metric = Amazon.CostExplorer.Metric.UNBLENDED_COST;
                string startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).ToString("yyyy-MM-dd");
                string endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddYears(1).AddDays(-1).ToString("yyyy-MM-dd");
                mapRequest.TimePeriod = new DateInterval()
                {
                    Start = startDate,
                    End = endDate
                };
                var response = await costExplorerRepository.GetCostForecast(mapRequest);
                var mapResponse = response.ForecastResultsByTime.Select(x => new CostUsageResponse()
                {
                    Name = Convert.ToDateTime(x.TimePeriod.Start).ToString("MMMM") + "-" + Convert.ToDateTime(x.TimePeriod.Start).Year,
                    Amount = Convert.ToDecimal(x.MeanValue),
                    Date = Convert.ToDateTime(x.TimePeriod.Start)
                });
                return mapResponse.ToList();
            }
            catch (DataUnavailableException ex)
            {
                return new List<CostUsageResponse>()
                {
                    new CostUsageResponse()
                    {
                        Name = DateTime.Now.ToString("MMMM")+"-"+DateTime.Now.Year.ToString(),
                        Amount=0,
                        Date=DateTime.Now
                    }
                };
            }
        }
        public async Task<List<CostUsageResponse>> GetForecastForCurrentMonth (CostUsageRequest costUsageRequest)
        {
            var mapRequest = mapper.Map<GetCostForecastRequest>(costUsageRequest);
            mapRequest.Granularity = Granularity.MONTHLY;
            mapRequest.Metric = Amazon.CostExplorer.Metric.UNBLENDED_COST;
            string startDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");//DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).ToString("yyyy-MM-dd"); //new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            if (endDate.Equals(startDate))
            {
                
            }
            mapRequest.TimePeriod = new DateInterval()  
            {
                Start = startDate,
                End = endDate
            };
            GetCostForecastResponse response = new GetCostForecastResponse();
            try
            {
                 response = await costExplorerRepository.GetCostForecast(mapRequest);
            }
            catch (DataUnavailableException ex)
            {
                return new List<CostUsageResponse>()
                {
                    new CostUsageResponse()
                    {
                        Name = "Current Month Cost:",
                        Amount=0.00M,
                        Custom="0",
                        Date=DateTime.Now
                    }
                };
            }
            var currentMonthCost = response.Total.Amount;
            var lastMonthCost = await GetLastMonthCost(costUsageRequest);
            var mapResponse = response.ForecastResultsByTime.Select(x => new CostUsageResponse()
            {
                Name = "Current Month Cost:",
                Amount = Convert.ToDecimal(currentMonthCost),
                Custom = Convert.ToString(Math.Round((Convert.ToDecimal(currentMonthCost)- Convert.ToDecimal(lastMonthCost)) / Convert.ToDecimal(currentMonthCost)*100,2))
            }) ;    
            return mapResponse.ToList();
        }
        public async Task<decimal> GetLastMonthCost(CostUsageRequest costUsageRequest)
        {
            costUsageRequest.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            costUsageRequest.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
           
            costUsageRequest.Granularity = "MONTHLY";
            var mapRequest = mapper.Map<GetCostAndUsageRequest>(costUsageRequest);
            var response = await costExplorerRepository.GetCostAndUsage(mapRequest);
            var mapResponse = response.ResultsByTime.SelectMany(x => x.Groups).Select(amt => Convert.ToDecimal(amt.Metrics[costUsageRequest.Metrics].Amount)).Sum();
            return mapResponse;
        }
    }
}
