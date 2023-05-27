using VertemNews.Application.Interfaces;
using VertemNews.Application.Services;
using VertemNews.Domain.Interfaces.Repositories;
using VertemNews.Domain.Interfaces.Repositories.Base;
using VertemNews.Extensions;
using VertemNews.Infra.Context;
using VertemNews.Infra.Repositories;
using VertemNews.Infra.Repositories.Base;

namespace VertemNews
{
    public class Startup
    {
        private static IConfiguration _configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(_configuration);
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<INewsApiIntegrationService, NewsApiIntegrationService>();
            services.AddDbContext<NewsContext>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddSwagger();
            services.AddControllers(options =>
                                    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            services.Configure<RouteOptions>(_configuration.GetSection(nameof(RouteOptions)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerBFF(env);

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