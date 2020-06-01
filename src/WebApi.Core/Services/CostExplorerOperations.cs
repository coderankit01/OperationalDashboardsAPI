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
            var mapResponse = response.ResultsByTime.SelectMany(x => x.Groups).GroupBy(x => x.Keys[response.GroupDefinitions.Count-1]).Select(z => new CostUsageResponse { Name = z.Key, Amount = z.Sum(c => Convert.ToDecimal(c.Metrics[costUsageRequest.Metrics].Amount)) });
            return mapResponse.ToList();
        }
    }
}
