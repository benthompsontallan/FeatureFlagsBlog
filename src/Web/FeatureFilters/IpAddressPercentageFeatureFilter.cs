using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.FeatureFilters
{
    [FilterAlias("IpAddressPercentage")]
    public class IpAddressPercentageFeatureFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IpAddressPercentageFeatureFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var settings = context.Parameters.Get<IpAddressPercentageFilterSettings>();

            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;

            int randomSeed = 0;

            if (ipAddress != null)
            {
                var ipBytes = ipAddress.GetAddressBytes();

                if (ipBytes.Length >= 3)
                {
                    int rawIp = 0;
                    rawIp ^= ipBytes[ipBytes.Length - 1];
                    rawIp ^= ipBytes[ipBytes.Length - 2] << 8;
                    rawIp ^= ipBytes[ipBytes.Length - 3] << 16;

                    randomSeed = rawIp;
                }
            }

            var random = new Random(randomSeed);

            bool isEnabled = random.Next(100) < settings.PercentageValue;

            return Task.FromResult(isEnabled);
        }
    }
}
