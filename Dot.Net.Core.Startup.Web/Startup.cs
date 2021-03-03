using FluentValidation.AspNetCore;
using Dot.Net.Core.Startup.Domain.Services;
using Dot.Net.Core.Startup.Domain.Validators;
using Dot.Net.Core.Startup.Web.Extensions;
using Dot.Net.Core.Startup.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Net.Http.Headers;
using System.Linq;
using Dot.Net.Core.Startup.Data.Entities;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;

namespace Dot.Net.Core.Startup.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Multilingual Support
            services.AddLocalization();


            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("de-DE"),
                        new CultureInfo("en-US")
                    };
                    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                    options.SetDefaultCulture(Configuration.GetSection("CurrentLanguage").Value);
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders = new[] { new RouteDataRequestCultureProvider { RouteDataStringKey = Configuration.GetSection("CurrentLanguage").Value , UIRouteDataStringKey = Configuration.GetSection("CurrentLanguage").Value } };
                   
                });

            services.AddHttpClient();

            services.ConfigureCors();
            services.ResolveDependencies();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dot.Net.Core.Startup.Web", Version = "v1"});

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            

            services.AddControllers(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.Filters.Add(typeof(ExceptionFilter));
                    options.Filters.Add(typeof(UnitOfWorkCommitFilter));
                    options.Filters.Add(typeof(ValidationFilter));
                })
                .AddNewtonsoftJson()
                .ConfigureApiBehaviorOptions(opt =>
                {
                    opt.SuppressModelStateInvalidFilter = true;
                })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApplicantValidator>());

            services.AddOData();
            AddFormatter(services);

            services.Configure<AppConfigurationData>(Configuration.GetSection("Settings"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dot.Net.Core.Startup.Web v1"));
            }

            app.UseRequestLocalization();
            app.UseHttpsRedirection();

            

            app.UseCors("default");

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.EnableDependencyInjection();
                endpoints.Expand().Select().Filter().OrderBy().Count().MaxTop(10);
                endpoints.MapControllers();
                endpoints.MapODataRoute("api", "api", GetEdmModel());
            });

        }

        private IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Applicant>("Applicants");
            return builder.GetEdmModel();
        }

        private void AddFormatter(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });
        }


    }
}
