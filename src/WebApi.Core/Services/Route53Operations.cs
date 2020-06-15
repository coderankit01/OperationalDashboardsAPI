using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationalDashboard.Web.Api.Core.Models.Response;
using OperationalDashboard.Web.Api.Infrastructure.Data.AWS;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
    public class Route53Operations : IResourceDetails
    {
        private static IRoute53Repository route53Repository { get; set; }
        public Route53Operations(IRoute53Repository _route53Repository)
        {
            route53Repository = _route53Repository;
        }
        public Route53Operations()
        {

        }
        public async Task<object> GetResourceDetails(MonitoringResourceRequest monitoringResourceRequest)
        {
            route53Repository = new Route53Repository();
            route53Repository.Region = monitoringResourceRequest.Region;
            var response = await route53Repository.GetHostedZones();
            var filterResponse = response.HostedZones.Where(x => monitoringResourceRequest.ResourceIds.Any(y => y.Equals(x.Id)));
            var mapResponse = filterResponse.Select(x => new Route53Response()
            {
                HostedZoneID = x.Id,
                Comment = x.Config?.Comment,
                DomainName = x.Name,
                RecordSetCount = x.ResourceRecordSetCount.ToString()
            });
            return mapResponse;
        }

        public async Task<MonitoringSummaryResponse> GetResources(string region)
        {
            route53Repository = new Route53Repository();
            route53Repository.Region = region;
            var response = await route53Repository.GetHostedZones();
            var resources = response.HostedZones.Select(x => x.Id).ToList();
            return new MonitoringSummaryResponse()
            {
                Label = "AWS/Route53",
                Count = resources.Count,
                ResourcesId = resources
            };
        }
    }
}
