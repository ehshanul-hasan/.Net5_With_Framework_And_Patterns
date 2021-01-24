using Hahn.ApplicatonProcess.December2020.Data.Context;
using Hahn.ApplicatonProcess.December2020.Data.UnitOfWork;
using Hahn.ApplicatonProcess.December2020.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Extensions
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
