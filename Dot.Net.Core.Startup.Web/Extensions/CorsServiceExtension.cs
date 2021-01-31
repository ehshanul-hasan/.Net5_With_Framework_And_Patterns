using Microsoft.Extensions.DependencyInjection;

namespace Dot.Net.Core.Startup.Web.Extensions
{
    public static class CorsServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("default",builder => 
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
    }
}
