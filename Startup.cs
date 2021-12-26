using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alou149.Data;
using Microsoft.EntityFrameworkCore;
//using alou149.Helper;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authentication;
using alou149.Handler;



namespace alou149
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
            
            services.AddAuthentication().
            AddScheme<AuthenticationSchemeOptions, MyAuthHandler>
            ("MyAuthentication", null); //.
            //AddScheme<AuthenticationSchemeOptions, AdminHandler>
            //("AdminAuthentication", null);
            services.AddDbContext<WebAPIDBContext>(options => options.UseSqlite(Configuration.GetConnectionString("WebAPIConnection")));
            services.AddControllers();
            services.AddScoped<IProductAPIRepo, DBProductAPIRepo>();
            services.AddScoped<IUsersAPIRepo, DBUsersAPIRepo>();
            services.AddScoped<IOrdersAPIRepo, DBOrdersAPIRepo>();

            services.AddAuthorization(options =>
            {
               // options.AddPolicy("AdminOnly", policy => policy.RequireClaim("admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireClaim("userName"));
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}


// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
