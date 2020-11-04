using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.RequestEcho;

namespace RequestEcho
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });

            services.Configure<ARMAuthOptions>(Configuration.GetSection(ARMAuthOptions.ARMAuth));

            services.AddSingleton<ISigningKeyProvider, SigningKeyProvider>();
            services.AddScoped<IProfilerTokenService, ProfilerTokenService>();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IARMCertCache, ARMCertCache>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // So that the certificate will be invoked at the beginning of the service.
            _ = app.ApplicationServices.GetRequiredService<IARMCertCache>().ValidThumbprints;

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
