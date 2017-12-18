using Cinode.Samples.Core;
using Cinode.Samples.Core.Abstractions;
using Cinode.Samples.Core.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cinode.Samples.Search
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<TokenOptions>(_configuration.GetSection("Token"));
            services.Configure<ApiOptions>(_configuration.GetSection("Api"));

            services.AddSingleton<IApiUriHelper, ApiUriHelper>();
            services.AddMemoryCache();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
                loggerFactory.AddConsole(_configuration.GetSection("Logging"));
                app.UseDeveloperExceptionPage();
            }


            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
