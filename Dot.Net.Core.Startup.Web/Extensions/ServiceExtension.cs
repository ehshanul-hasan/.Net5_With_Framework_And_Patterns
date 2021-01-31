using Dot.Net.Core.Startup.Data.Context;
using Dot.Net.Core.Startup.Data.UnitOfWork;
using Dot.Net.Core.Startup.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dot.Net.Core.Startup.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<ICountryService, CountryService>();
        }
    }
}
