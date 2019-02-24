using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace api
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

            //nécessite namespace Microsoft.EntityFrameworkCore
            services.AddDbContext<WebApiDemoContext>(config=>config.UseSqlServer(Configuration.GetConnectionString("Labo3")));
            services.AddTransient<DataAccess>();
            // Configuration d'auto mapper: http://docs.automapper.org/en/stable/Configuration.html 
            AutoMapper.Mapper.Initialize(config=>config.AddProfile<Infrastructure.MappingProfile>());

            // nécessite l'installation du package AutoMapper.Extensions.Microsoft.DependencyInjection
            // et l'ajout du namespace AutoMapper. 
            // permet de configurer le container servant à l'injection des dépendances afin qu'il soit capable de fournir une implémentation de IMapper à tous les controllers le nécessitant. 
            services.AddAutoMapper();    

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
