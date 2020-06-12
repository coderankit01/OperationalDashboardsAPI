using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
   public interface IDynamoDBRepository
    {
        string Region { get; set; }
        Task<ListTablesResponse> GetDynamoDBList();
        Task<ListTablesResponse> GetDynamoDBList(ListTablesRequest request);
        Task<List<DescribeTableResponse>> GetDynamoDBList(List<string> tables);
    }
}
