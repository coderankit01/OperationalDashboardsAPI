using Amazon.CostExplorer.Model;
using AutoMapper;
using OperationalDashboard.Web.Api.Core.Interfaces;
using OperationalDashboard.Web.Api.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Services
{
    public class CostRecommendationsOperations: ICostRecommendationsOperations
    {

        private static IMapper mapper { get; set; }
        private static ICostExplorerRepository costExplorerRepository { get; set; }
        public CostRecommendationsOperations(IMapper _mapper, ICostExplorerRepository _costExplorerRepository)
        {
            mapper = _mapper;
            costExplorerRepository = _costExplorerRepository;
        }
        public async Task<GetRightsizingRecommendationResponse> GetRightsizingRecommendation()
        {
            GetRightsizingRecommendationRequest rightsizingRecommendationRequest = new GetRightsizingRecommendationRequest();
            //rightsizingRecommendationRequest.Filter = new Expression()
            //{
            //    Dimensions = new DimensionValues()
            //    {
            //        Values = new List<string>()
            //        {
            //            //"holds a string",
            //            //"ksajfskf"
            //        }
            //    },
            //    Tags = new TagValues()
            //    {
            //        Values = new List<string>()
            //        {
            //            "tagvalues",
            //            "kfhasfkj"
            //        }
            //    },
            //    CostCategories = new CostCategoryValues()
            //    {
            //        Values = new List<string>
            //        {
            //            "kjdsfskjd",
            //            "jfksa"
            //        },
            //        Key = "hfkjdf"
            //    }
            //};
            var response = await costExplorerRepository.GetRightsizingRecommendation(rightsizingRecommendationRequest);
            var totalCost = response.RightsizingRecommendations.Select(x => Convert.ToDecimal(x.CurrentInstance.MonthlyCost)).Sum();
            var monthlyEstimationCost = response.RightsizingRecommendations.SelectMany(x => x.ModifyRecommendationDetail.TargetInstances).Select(y => Convert.ToDecimal(y.EstimatedMonthlyCost)).Sum();
            return response;
        }
    }
}
