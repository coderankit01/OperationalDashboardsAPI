using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Amazon.CloudWatch.Model;
using Amazon.S3.Model.Internal.MarshallTransformations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationalDashboard.Web.Api.Core.Constants;
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
            var filterMetrics = metrics.Where(x => monitoringRequest.ResourceIds.Any(y =>  x.Dimensions.Any(z =>  z.Value.Equals(y)))).
               
                Select(z => new Metric()
                {
                    MetricName = z.MetricName,
                    Namespace = z.Namespace,
                    Dimensions = z.Dimensions.Where(y => y.Name.Equals(MonitoringConstants.nameSpaceIdentifiers[monitoringRequest.NameSpace])).ToList()
                }).ToList();
            
            var response = await monitoringOperations.GetMetricsData(monitoringRequest, filterMetrics);
            var mapResponse = monitoringOperations.MapResponse(response.MetricResponse, MetricType, monitoringRequest.Limit);
            return Ok(mapResponse);
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