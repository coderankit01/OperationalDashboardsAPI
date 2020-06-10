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
   public class EBSOperations : IResourceDetails
    {
        private static IEBSRepository eBSRepository { get; set; }
        public EBSOperations(IEBSRepository _eBSRepository)
        {
            eBSRepository = _eBSRepository;
        }
        public EBSOperations()
        {

        }

        public async Task<object> GetResourceDetails(MonitoringResourceRequest monitoringResourceRequest)
        {
            eBSRepository = new EBSRepository(); 
           
            eBSRepository.Region = monitoringResourceRequest.Region;
            var response = await eBSRepository.GetEBS(new Amazon.EC2.Model.DescribeVolumesRequest() { VolumeIds = monitoringResourceRequest.ResourceIds });
            var mapResponse = response.Volumes;
            return mapResponse;
        }

        public async Task<MonitoringSummaryResponse> GetResources(string region)
        {
            eBSRepository = new EBSRepository();
            eBSRepository.Region = region;
            var response = await eBSRepository.GetEBS();
            var resources = response.Volumes.Select(x => x.VolumeId).ToList();
            return new MonitoringSummaryResponse()
            {
                Label = "AWS/EBS",
                Count = resources.Count,
                ResourcesId = resources
            };

        }
    }
}
