using Amazon;
using Amazon.Route53;
using Amazon.Route53.Model;
using OperationalDashboard.Web.Api.Infrastructure.Base;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Data.AWS
{
   public class Route53Repository:AWSBaseClient, IRoute53Repository
    {
        public string Region { get; set; }
        public async Task<ListHostedZonesResponse> GetHostedZones()
        {
            using (var amazonRoute53Client= new AmazonRoute53Client(awsCredentials, RegionEndpoint.GetBySystemName(Region)))
            {
                var response = await amazonRoute53Client.ListHostedZonesAsync();
                return response;
            }
        }
    }
}
