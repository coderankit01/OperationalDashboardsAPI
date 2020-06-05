using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;

namespace OperationDashboard.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private static IMonitoringOperations monitoringOperations { get; set; }
        public MonitoringController(IMonitoringOperations _monitoringOperations)
        {
            monitoringOperations = _monitoringOperations;
        }

        [HttpPost]
        public async Task<IActionResult> GetMetrics([FromBody]MonitoringRequest monitoringRequest)
        {
            var metrics = await monitoringOperations.GetMetrics(monitoringRequest.NameSpace, monitoringRequest.Metrics);
            var response = await monitoringOperations.GetMetricsData(monitoringRequest, metrics);
            return Ok(response);
        }
        

    }
}