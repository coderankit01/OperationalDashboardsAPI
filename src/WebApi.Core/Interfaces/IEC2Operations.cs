using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OperationalDashboard.Web.Api.Core.Interfaces
{
   public interface IEC2Operations
    {
        Task<List<string>> GetRegions();
    }
}
