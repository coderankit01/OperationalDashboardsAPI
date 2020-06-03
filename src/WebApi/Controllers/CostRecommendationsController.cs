using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetMonthlyCostRecommendation()
        {
            var response = await costRecommendationsOperations.GetRightsizingRecommendation();
            return Ok(response);
        }
    }
}