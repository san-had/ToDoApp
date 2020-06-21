using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using ToDo.Domain;
using ToDo.Domain.Converters;
using ToDo.Domain.Database.Providers;
using ToDo.Domain.Repositories;
using ToDo.Extensibility;
using ToDo.Service;

namespace ToDo.UI
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
            services.AddScoped<MsSqlLiteDatabaseContext>();
            services.AddScoped<IToDoEntityConverter, ToDoEntityConverter>();
            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddScoped<IToDoService, ToDoService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Init();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/ToDo/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ToDo}/{action=Index}/{id?}");
            });
        }

        private void Init()
        {
            var kernel = new StandardKernel();
            kernel.Load<ServiceNinjectModule>();
            kernel.Load<DomainNinjectModule>();
        }
    }
}