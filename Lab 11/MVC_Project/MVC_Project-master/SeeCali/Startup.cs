using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SeeCali.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SeeCali
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FormattingService>();

            services.AddTransient<FeatureToggles>(x => new FeatureToggles {
                DeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
                });

            services.AddDbContext<BlogDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("BlogDataContext");
                options.UseSqlServer(connectionString);
            });

            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("IdentityDataContext");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();

            

            services.AddMvc();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            FeatureToggles features)
        {
            
            app.UseExceptionHandler("/error.html");

            if (features.DeveloperExceptions)//configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions"))//configuration.GetValue<bool>("EnableDeveloperExceptions"))//configuration["EnableDeveloperExceptions"] == "True")//env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR!");
                await next();
            });

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute( "default" , "{controller=Home}/{action=Index}/{id?}");

            });
            app.UseFileServer();


            /*
             * @model IEnumerable<SeeCali.Models.MonthlySpecial>

<h1>Monthly Specials</h1>

@foreach (var special in Model)
{
    <h2 class="top">
        <img src="/images/@(special.Key)_bug.gif" alt="@special.Name" width="75" height="75">
        @special.Name
    </h2>
    <p>
        @special.Type <br>
        <a href="/tours/@(special.Key).htm">$@special.Price</a>
    </p>
}*/

           /* Ch. 1
            *app.Use(async(context, next) =>
            {
                if (context.Request.Path.Value.StartsWith("/hello"))
                {
                    await context.Response.WriteAsync("Hello ASP.NET Core!");
                }
                await next();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("\nHow are you?");
            });*/
        }
    }
}
