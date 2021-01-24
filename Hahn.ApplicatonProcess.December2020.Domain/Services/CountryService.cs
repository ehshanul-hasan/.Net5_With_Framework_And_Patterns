using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly AppConfigurationData _appConfigurationData;
        private readonly IHttpClientFactory _clientFactory;

        public CountryService(IOptionsSnapshot<AppConfigurationData> appConfigurationData, IHttpClientFactory clientFactory)
        {
            _appConfigurationData = appConfigurationData.Value;
            _clientFactory = clientFactory;
        }
        public async Task<bool> IsValidCountry(string countryname, CancellationToken cancellationToken = default)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                string url = _appConfigurationData.countryapi.Replace("{{name}}", countryname);
                var result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }


    public class AppConfigurationData
    {
        public string countryapi { get; set; }
    }

}
