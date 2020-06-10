using Amazon.RDS.Model;
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
   public class RDSOperations: IResourceDetails
    {
        private static IRDSRepostitory rDSRepostitory { get; set; }
        public RDSOperations(IRDSRepostitory _rDSRepostitory)
        {
            rDSRepostitory = _rDSRepostitory;
        }
        public RDSOperations()
        {

        }

        public async Task<object> GetResourceDetails(MonitoringResourceRequest monitoringResourceRequest)
        {
            rDSRepostitory = new RDSRepostitory();
            rDSRepostitory.Region = monitoringResourceRequest.Region;
            var instanceRequest = new DescribeDBInstancesRequest()
            {
                Filters = new List<Amazon.RDS.Model.Filter>()
                {
                 new Amazon.RDS.Model.Filter()
                 {
                     Name="db-instance-id",
                     Values =monitoringResourceRequest.ResourceIds
                 }
                }
            };
            var response = await rDSRepostitory.GetDatabaseDetails(instanceRequest);
            var value = response.DBInstances;
            return value;
        }
        public async Task<MonitoringSummaryResponse> GetResources(string region)
        {
            rDSRepostitory = new RDSRepostitory();
            rDSRepostitory.Region = region;
            var response=await  rDSRepostitory.GetDatabases();
            var resources= response.DBInstances.Select(x => x.DBInstanceIdentifier).ToList();
            return new MonitoringSummaryResponse()
            {
                Label = "AWS/RDS",
                Count = resources.Count,
                ResourcesId = resources
            };
        }
    }
}
