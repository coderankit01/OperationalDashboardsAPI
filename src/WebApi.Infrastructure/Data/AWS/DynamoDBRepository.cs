using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using OperationalDashboard.Web.Api.Infrastructure.Base;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
   public class DynamoDBRepository:AWSBaseClient, IDynamoDBRepository
    {
        public string Region { get; set; }
        public async Task<ListTablesResponse> GetDynamoDBList()
        {
            using (var dynamoDBClient = new AmazonDynamoDBClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await dynamoDBClient.ListTablesAsync();
                return response;
            }
        }
        public async Task<ListTablesResponse> GetDynamoDBList(ListTablesRequest request)
        {
            using (var dynamoDBClient = new AmazonDynamoDBClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await dynamoDBClient.ListTablesAsync(request);
                return response;
            }
        }
        public async Task<List<DescribeTableResponse>> GetDynamoDBList(List<string> tables)
        {
            List<DescribeTableResponse> tableResponses = new List<DescribeTableResponse>();
            using (var dynamoDBClient = new AmazonDynamoDBClient(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                foreach(var table in tables)
                {
                    var response = await dynamoDBClient.DescribeTableAsync(table);
                    tableResponses.Add(response);
                }
                return tableResponses;
            }
        }
    }
}
