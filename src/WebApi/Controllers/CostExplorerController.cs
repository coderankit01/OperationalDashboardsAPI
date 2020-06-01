﻿using System;
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
    }
}