using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet]
        public async Task<IActionResult> GetCostUsage([FromBody]CostUsageRequest costUsageRequest)
        {
            var response = await costExplorerOperations.GetCostUsage(costUsageRequest);
            return Ok(response);
        }

        [HttpGet("CostByMonth")]
        public async Task<IActionResult> GetCostByMonth( [FromBody]CostUsageRequest costUsageRequest)
        {
            var response = await costExplorerOperations.GetCostByMonth(costUsageRequest);
            return Ok(response);
        }


        [HttpGet("CurrentMonth")]
        public async Task<IActionResult> GetCurrentMonthCost([FromBody]CostUsageRequest costUsageRequest)
        {
            var response = await costExplorerOperations.GetCurrentMonthCost(costUsageRequest);
            return Ok(response);
        }

        [HttpGet("CurrentYear")]
        public async Task<IActionResult> GetCurrentYearCost([FromBody]CostUsageRequest costUsageRequest)
        {
            var response = await costExplorerOperations.GetCurrentYearCost(costUsageRequest);
            return Ok(response);
        }

    }
}