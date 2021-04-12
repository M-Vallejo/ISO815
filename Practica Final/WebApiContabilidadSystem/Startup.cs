using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiContabilidadSystem.Data;
using WebApiContabilidadSystem.Models;
using WebApiContabilidadSystem.Utils;

namespace WebApiContabilidadSystem
{
    public class Startup
    {
        private const string CORS_POLICE = "AllowVueJs";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddControllers();
            services.AddDbContext<ContabilidadDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddCors(option => 
            {
                option.AddPolicy(CORS_POLICE, police =>
                {
                    police
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                app.UseHsts();
                app.UseSpaStaticFiles();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(CORS_POLICE);
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            });

            // Nos aseguramos de que la base de datos exista y, encaso contrario, la creamos con un usuario por defecto.
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<ContabilidadDbContext>();
            if (context.Database.EnsureCreated())
            {
                context.Usuario.Add(new USUARIO
                {
                    NOMBRE_USUARIO = "admin",
                    CLAVE = Security.Encrypt("123456"),
                    NOMBRE = "Administrador",
                    APELLIDOS = "Gonzalez",
                    CORREO = "correo@ejemplo.com",
                    ROL = 1,
                    FECHA_CREACION = DateTime.Now,
                    ESTADO = 1
                });

                context.SaveChanges();
            }
        }
    }
}
