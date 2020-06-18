using Amazon.DynamoDBv2.Model;
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
    public class DynamoDBOperations : IResourceDetails
    {
        private static IDynamoDBRepository dynamoDBRepository { get; set; }
        public DynamoDBOperations(IDynamoDBRepository _dynamoDBRepository)
        {
            dynamoDBRepository = _dynamoDBRepository;
        }
        public DynamoDBOperations()
        {

        }
        public async Task<object> GetResourceDetails(MonitoringResourceRequest monitoringResourceRequest)
        {
            dynamoDBRepository = new DynamoDBRepository();
            dynamoDBRepository.Region = monitoringResourceRequest.Region;
           
            var response = await dynamoDBRepository.GetDynamoDBList(monitoringResourceRequest.ResourceIds);
            var filterResponse = response.Select(x=>x.Table).Select(x => new DynamoDBResponse() {
                     Indexes=String.Join(",",x.GlobalSecondaryIndexes?.Select(y=>y.IndexName)),
                     PartitionKey=x.KeySchema?.FirstOrDefault(y=>y.KeyType.Value.Equals("partition key"))?.AttributeName,
                     Name=x.TableName,
                     SortKey= x.KeySchema?.FirstOrDefault(y => y.KeyType.Value.Equals("sort key"))?.AttributeName,
                     Status=x.TableStatus?.Value,
                     TableSize=x.TableSizeBytes.ToString(),
                     CreatedDate=x.CreationDateTime.ToString()
            } );
            return filterResponse;
        }

        public async Task<MonitoringSummaryResponse> GetResources(string region)
        {
            dynamoDBRepository = new DynamoDBRepository();
            dynamoDBRepository.Region = region;
            var response = await dynamoDBRepository.GetDynamoDBList();
            var resources = response.TableNames;
            return new MonitoringSummaryResponse()
            {
                Label = "AWS/DynamoDB",
                Count = resources.Count,
                ResourcesId = resources
            };
           
        }
    }
}
