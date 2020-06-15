using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationDashboard.Web.Api.Identity;

namespace OperationDashboard.Web.Api.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class ChecksController : ControllerBase
    {
        private static ITrustedAdvisorOperations trustedAdvisorOperations { get; set; }
        public ChecksController( ITrustedAdvisorOperations _trustedAdvisorOperations)
        {
            trustedAdvisorOperations = _trustedAdvisorOperations;
        }
        [HttpGet("Summary")]
        public async Task<IActionResult> CheckStatusCount([FromQuery]string Category)
        {
            var response = await trustedAdvisorOperations.GetAdvisorySummary(Category);
            return Ok(response);
        }
        [HttpGet("Recommendations")]
        public async Task<IActionResult> GetResourceRecommendation([FromQuery]string Category)
        {
           var response = await trustedAdvisorOperations.GetResourceRecommendation(Category);
            return Ok(response);
        }
        [HttpGet("Resources")]
        public async Task<IActionResult> GetResourceDetails([FromQuery]string CheckId)
        {
            var response = await trustedAdvisorOperations.GetResourceDetails(CheckId);
            return Ok(response);
        }
    } 
}