using Amazon.Route53.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Infrastructure.Interfaces
{
   public interface IRoute53Repository
    {
        string Region { get; set; }
        Task<ListHostedZonesResponse> GetHostedZones();
    }
}
