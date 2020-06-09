using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CostExplorer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Core.Models.Request;
using OperationDashboard.Web.Api.Identity;

namespace OperationDashboard.Web.Api.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class CostExplorerController : ControllerBase
    {
        private static ICostExplorerOperations costExplorerOperations { get; set; }
        public CostExplorerController(ICostExplorerOperations _costExplorerOperations)
        {
            costExplorerOperations = _costExplorerOperations;
        }
        [HttpGet("Ping")]
        public async Task<IActionResult> Get()
        {
            
            return Ok("Connected");
        }
        [HttpPost]
        public async Task<IActionResult> GetCostUsage([FromBody]CostUsageRequest costUsageRequest)
        {
            var response = await costExplorerOperations.GetCostUsage(costUsageRequest);
            return Ok(response);
        }
        
        [HttpGet("Accounts")]
        public async Task<IActionResult> GetLinkedAccounts()
        {
            var response = await costExplorerOperations.GetLinkedAccounts();
            return Ok(response);
        }

        [HttpPost("CostByMonth")]
        public async Task<IActionResult> GetCostByMonth( [FromBody]CostUsageRequest costUsageRequest)
        {
            var response = await costExplorerOperations.GetCostByMonth(costUsageRequest);
            return Ok(response);
        }
        
        [HttpPost("CurrentMonth")]
        public async Task<IActionResult> GetCurrentMonthCost([FromBody]CostUsageRequest costUsageRequest)
        {
            var response = await costExplorerOperations.GetCurrentMonthCost(costUsageRequest);
            return Ok(response);
        }

        [HttpPost("CurrentYear")]
        public async Task<IActionResult> GetCurrentYearCost([FromBody]CostUsageRequest costUsageRequest)
        {
            var response = await costExplorerOperations.GetCurrentYearCost(costUsageRequest);
            return Ok(response);
        }

        [HttpPost("Forecast")]
        public async Task<IActionResult> GetMonthlyCostPrediction([FromBody]CostUsageRequest costUsageRequest)
        {
            var response = await costExplorerOperations.GetCostForecast(costUsageRequest);
            return Ok(response);
        }
        [HttpPost("CurrentMonthForecast")]
        public async Task<IActionResult> GetCurrentMonthForecasts([FromBody]CostUsageRequest costUsageRequest)
        {
            var response = await costExplorerOperations.GetForecastForCurrentMonth(costUsageRequest);
            return Ok(response);
        }

    }
}