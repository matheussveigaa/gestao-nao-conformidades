using AutoMapper;
using FluentValidation.AspNetCore;
using GestaoDeNaoConformidades.Application.Commands.InserirNaoConformidade;
using GestaoDeNaoConformidades.Infrastructure.Data;
using GestaoDeNaoConformidades.Infrastructure.Data.Repositories;
using GestaoDeNaoConformidades.Middlewares;
using GestaoDeNaoConformidades.Rest.Map;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace GestaoDeNaoConformidades
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assemblyApplication = AppDomain.CurrentDomain.Load("GestaoDeNaoConformidades");

            services
                .AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssembly(assemblyApplication);
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddTransient<GlobalExceptionHandlerMiddleware>();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            // Auto registrar todas as interfaces e implementações de repository
            services.ForInterfacesMatching("^I[a-zA-z]+Repository$")
                    .OfAssemblies(typeof(NaoConformidadeRepository).GetTypeInfo().Assembly)
                    .AddScoped();

            services.AddMediatR(assemblyApplication);

            ConfigureDbContext(services);
            ConfigureSwagger(services);
            ConfigureAutoMapper(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseHttpsRedirection();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gestão de não conformidades V1");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<GestaoNaoConformidadesDbContext>(option => option.UseNpgsql(Configuration.GetConnectionString("PGSQLConnection")));
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Gestão de não conformidades API",
                    Version = "v1",
                    Description = "Exemplo de API REST"
                });
            });
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
        }
    }
}
