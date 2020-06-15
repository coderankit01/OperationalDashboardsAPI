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
    public class TrustedAdvisorController : ControllerBase
    {
        private static ITrustedAdvisorOperations trustedAdvisorOperations { get; set; }
        public TrustedAdvisorController( ITrustedAdvisorOperations _trustedAdvisorOperations)
        {
            trustedAdvisorOperations = _trustedAdvisorOperations;
        }
        [HttpGet("Checks")]
        public async Task<IActionResult> GetChecks([FromQuery]string language)
        {
            var response = await trustedAdvisorOperations.TrustedAdvisorChecks(language);
            return Ok(response);
        }
        [HttpGet("AdvisorCheck")]
        public async Task<IActionResult> AdvisorCheck([FromQuery]string checkID, string language)
        {
            var response = await trustedAdvisorOperations.TrustedAdvisorCheckResult(checkID, language);
            return Ok(response);
        }
        [HttpGet("AdvisorCheckSummary")]
        public async Task<IActionResult> CheckSummary([FromBody] List<string> checkIDs)
        {
            var response = await trustedAdvisorOperations.TrustedAdvisorCheckSummary(checkIDs);
            return Ok(response);
        }
        [HttpGet("CheckStatusCount")]
        public async Task<IActionResult> CheckStatusCount([FromQuery]string Category)
        {
            var response = await trustedAdvisorOperations.GetStateCount(Category);
            return Ok(response);
        }
        [HttpGet("GetChecksList")]
        public async Task<IActionResult> ChecksList([FromQuery]string Category)
        {
           var response = await trustedAdvisorOperations.GetRecommendations(Category);
            return Ok(response);
        }
    } 
}