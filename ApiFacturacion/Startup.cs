using ApiFacturacion.Entities;
using ApiFacturacion.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace ApiFacturacion
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
            //services.AddCors();
            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<FacturaEnc, FacturaEncDTO>().ReverseMap();
                configuration.CreateMap<FacturaDet, FacturaDetDTO>().ReverseMap();
                configuration.CreateMap<FacturaEnc, FacturaDTOAnulacion>().ReverseMap();
                configuration.CreateMap<Producto, ProductoDTO>().ReverseMap();
            }, typeof(Startup));
            services.AddResponseCaching();
            services.AddDbContext<BdFacturacionContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Mi Web API",
                    Description = "Esta es una descripción del Web API",
                    TermsOfService = new Uri("https://www.asdasdaasd.com/"),
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("http://bfy.tw/4nqh")
                    },
                    Contact = new OpenApiContact()
                    {
                        Name = "Duvan Gonzalez",
                        Email = "asdasdas@fca.caomc",
                        Url = new Uri("https://asdasdsd.blog/")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
