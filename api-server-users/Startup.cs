using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace api_server_users
{
    /// <summary>
    /// Classe de inicialização
    /// </summary>
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            #region Swagger
            services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("v1", new Info()
                {
                    Title = "API - Servidor de Usuários",
                    Version = "v1"
                });

                // Configuração para exibir comentários
                var projectPath = PlatformServices.Default.Application.ApplicationBasePath;
                var projectName = $"{PlatformServices.Default.Application.ApplicationName}.xml";
                var xmlFilePathComment = Path.Combine(projectPath, projectName);

                conf.IncludeXmlComments(xmlFilePathComment);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(conf => {
                conf.SwaggerEndpoint("/swagger/v1/swagger.json", "API - Servidor de Usuários");
                conf.RoutePrefix = "swagger";
            });
        }
    }
}
