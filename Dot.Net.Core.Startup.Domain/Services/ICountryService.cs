using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dot.Net.Core.Startup.Domain.Services
{
    public interface ICountryService
    {
        Task<bool> IsValidCountry(string countryname, CancellationToken cancellationToken = default);
    }
}
