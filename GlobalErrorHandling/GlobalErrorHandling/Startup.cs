using GlobalErrorHandling.Extensions;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalErrorHandling
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            NLog.LogManager.LoadConfiguration("./nlog.config");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLoggerService();
            services.ConfigureCors();
            services.ConfigureIISIntegration();

            services.ConfigureMySqlContext(Configuration);
            services.ConfigureRepositoryWrapper();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.ConfigureExceptionHandler(logger);
            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            app.UseForwardedHeaders(
                new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.All
                });
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
