using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[controller]/[action]")]
    [FeatureGate(FeatureFlags.ClearancePage)]
    public class ClearanceController : Controller
    {
        public IActionResult Sales()
        {
            return View();
        }
    }
}
