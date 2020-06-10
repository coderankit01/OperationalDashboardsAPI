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
        [HttpGet("Savings")]
        public async Task<IActionResult> GetMonthlyCost()
        {
            var response = await costRecommendationsOperations.GetCurrentCostRecomNSavingsCost();
            return Ok(response);
        }
        //getting output but also fetches empty variab;les of CostRecommendationResponse Class
        [HttpGet("CPUusage")]
        public async Task<IActionResult> UnusedCPUusage()
        {
            var response = await costRecommendationsOperations.GetCPUusageDetails();
            return Ok(response);
        }

       //getting RecommendationResponse in output
        [HttpGet("Activities")]
        public async Task<IActionResult> RecommendationActivities()
        {
            var response = await costRecommendationsOperations.GetHighRecommActivities();
            return Ok(response);
        }

        [HttpGet("Summary")]
        public async Task<IActionResult> RecommendationsSummary()
        {
            var response = await costRecommendationsOperations.GetSummaryData();
            return Ok(response);
        }

    }
}