using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3.Model.Internal.MarshallTransformations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationalDashboard.Web.Api.Core.Extensions;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationDashboard.Web.Api.Identity;

namespace OperationDashboard.Web.Api.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]    
    public class MonitoringController : ControllerBase
    {
        private static IMonitoringOperations monitoringOperations { get; set; }
        private static IEC2Operations eC2Operations { get; set; }
        public MonitoringController(IMonitoringOperations _monitoringOperations, IEC2Operations _eC2Operations)
        {
            monitoringOperations = _monitoringOperations;
            eC2Operations = _eC2Operations;
        }

        [HttpPost("Metrics")]
        public async Task<IActionResult> GetMetrics([FromBody]MonitoringRequest monitoringRequest,[FromQuery]string MetricType)
        {
            if (!ValidationHelper.IsValidMonitorRequest(monitoringRequest, MetricType, out string message))
            {
                return BadRequest(new { Message = message });
            }
            var metrics = await monitoringOperations.GetMetrics(monitoringRequest.Region, monitoringRequest.NameSpace, monitoringRequest.Metrics);
            var filterMetrics = metrics.Where(x => monitoringRequest.ResourceIds.Any(y => x.Dimensions.Any(z => z.Value.Equals(y)))).ToList() ;
            var response = await monitoringOperations.GetMetricsData(monitoringRequest, filterMetrics);
            if (!response.MetricResponse.Any())
            {
                return NoContent();
            }
            var mapResponse = monitoringOperations.MapResponse(response.MetricResponse, MetricType, monitoringRequest.Limit);
            return Ok(mapResponse);
        }

        [HttpPost("Metric")]
        public async Task<IActionResult> GetMetric([FromBody]MonitoringRequest monitoringRequest)
            {
            if (!ValidationHelper.IsValidateGetMetrics(monitoringRequest, out string message))
            {
                return BadRequest(new { Message = message });
            }
            var metrics = await monitoringOperations.GetMetrics(monitoringRequest.Region, monitoringRequest.NameSpace, monitoringRequest.Metrics,monitoringRequest.ResourceIds.FirstOrDefault());
            var response = await monitoringOperations.GetMetricsData(monitoringRequest, metrics);

            return Ok(response);
        }
        [HttpGet("Summary")]
        public async Task<IActionResult> GetResourceSummary([FromQuery]string Namespace,[FromQuery]string region)
        {
            if (!ValidationHelper.IsValidateSummary(Namespace, out string message))
            {
                return BadRequest(new { Message = message });
            }
            IResourceDetails resourceDetails = ReflectionHelper.GetInstanceByNamespace(Namespace);
            var response = await resourceDetails.GetResources(region);
            return Ok(response);
        }

        [HttpPost("Resources")]
        public async Task<IActionResult> GetResourceDetails([FromBody]MonitoringResourceRequest monitoringResourceRequest)
        {
            if(!ValidationHelper.IsValidateResources(monitoringResourceRequest, out string message))
            {
                return BadRequest(new { Message = message });
            }
            IResourceDetails resourceDetails = ReflectionHelper.GetInstanceByNamespace(monitoringResourceRequest.Namespace);
            var response= await resourceDetails.GetResourceDetails(monitoringResourceRequest);
            return Ok(response);
        }

        [HttpGet("Region")]  
        public async Task<IActionResult> GetRegions()
        {
            var response = await eC2Operations.GetRegions();
            return Ok(response);
        }
    }
}