using Amazon.EC2.Model;
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
    public class EC2Operations : IResourceDetails, IEC2Operations
    {
        private static IEC2Repository eC2Repository { get; set; }
        public EC2Operations(IEC2Repository _eC2Repository)
        {
            eC2Repository = _eC2Repository;
        }
        public EC2Operations()
        {
          
        }
        public async Task<object> GetResourceDetails(MonitoringResourceRequest monitoringResourceRequest)
        {
            eC2Repository = new EC2Repository();
            eC2Repository.Region = monitoringResourceRequest.Region;
            var instanceRequest = new DescribeInstancesRequest()
            {
                InstanceIds = monitoringResourceRequest.ResourceIds,
            };
            var response = await eC2Repository.GetInstanceDetails(instanceRequest);
            var value = response.Reservations.SelectMany(x=>x.Instances);
            return value;
        }

        public async Task<MonitoringSummaryResponse> GetResources(string region)
        {
            eC2Repository = new EC2Repository();
            eC2Repository.Region = region;
            var response = await eC2Repository.GetInstances();
            var resources = response.Reservations.SelectMany(x => x.Instances).Select(x=>x.InstanceId).ToList();
            return new MonitoringSummaryResponse()
            {
                Label = "AWS/EC2",
                Count = resources.Count,
                ResourcesId = resources
            };
        }
        public async Task<List<string>> GetRegions()
        {
            eC2Repository.Region = "us-east-1";
            var response = await eC2Repository.GetRegions();
            var filterResponse = response.Regions.Select(x => x.RegionName).ToList();
            return filterResponse;
        }
    }
}
