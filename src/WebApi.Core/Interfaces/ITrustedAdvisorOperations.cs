﻿using Amazon.AWSSupport.Model;
using OperationalDashboard.Web.Api.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Interfaces
{
    public interface ITrustedAdvisorOperations
    {
        Task<DescribeTrustedAdvisorChecksResponse> TrustedAdvisorChecks(string language);
        Task<DescribeTrustedAdvisorCheckResultResponse> TrustedAdvisorCheckResult(string checkID, string language);
        Task<DescribeTrustedAdvisorCheckSummariesResponse> TrustedAdvisorCheckSummary(List<string> checkIDs);
        Task<StateCountResponse> GetStateCount(string Category);
        Task<List<CheckRecommResponse>> GetRecommendations(string Category);
    }
}
