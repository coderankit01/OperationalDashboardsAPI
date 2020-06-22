using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationalDashboard.Web.Api.Core.Interfaces;

namespace OperationDashboard.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostRecommendationsController : ControllerBase
    {
        private static ICostRecommendationsOperations costRecommendationsOperations { get; set; }
        public CostRecommendationsController(ICostRecommendationsOperations _costRecommendationsOperations)
        {
            costRecommendationsOperations = _costRecommendationsOperations;
        }
        [HttpGet]
        public async Task<IActionResult> GetCostSummaryDetails()
        {
            var response = await costRecommendationsOperations.GetCostSummaryDetails();
            return Ok(response);
        }
        [HttpGet("Summary")]
        public async Task<IActionResult> RecommendationsSummary()
        {
            var response = await costRecommendationsOperations.GetCostSummary();
            return Ok(response);
        }
        [HttpGet("CPU")]
        public async Task<IActionResult> GetUsedVsUnusedCpu()
        {
            var response = await costRecommendationsOperations.GetUsedVsUnusedCpu();
            return Ok(response);
        }
        [HttpGet("RecommendCpu")]
        public async Task<IActionResult> GetCpuVsRecommendCpuUsage()
        {
            var response = await costRecommendationsOperations.GetCpuVsRecommendCpuUsage();
            return Ok(response);
        }
        [HttpGet("Savings")]
        public async Task<IActionResult> GetCostVsSavings()
        {
            var response = await costRecommendationsOperations.GetCostVsSavings();
            return Ok(response);
        }
        //getting RecommendationResponse in output
        [HttpGet("Activities")]
        public async Task<IActionResult> RecommendationActivities()
        {
            var response = await costRecommendationsOperations.GetHighRecommActivities();
            return Ok(response);
        }

    }
}