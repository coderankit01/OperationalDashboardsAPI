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
            var mapResponse = response.ResultsByTime.SelectMany(x => x.Groups).Select(amt => Convert.ToDecimal(amt.Metrics[costUsageRequest.Metrics].Amount)).Sum();
            return new List<CostUsageResponse>() {
                         new CostUsageResponse()
                                  {
                                        Name = DateTime.Now.ToString("MMMM")+ "-" + DateTime.Now.Year.ToString(),
                                        Amount = mapResponse
                                  }
            };
        }
    }
}
