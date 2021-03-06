using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Weather.BLL.Services;
using Weather.Core.Interfaces;

namespace Weather.WebAPI
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

			services.AddScoped<IWeatherService, WeatherapiWeatherService>(); 
			services.AddSwaggerGen();
			services.ConfigureSwaggerGen(options =>
			{
				//Determine base path for the application.
				var basePath = PlatformServices.Default.Application.ApplicationBasePath;

				//Set the comments path for the swagger json and ui.
				var xmlPath = Path.Combine(basePath, "Weather.WebAPI.xml");
				options.IncludeXmlComments(xmlPath);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseSwagger(c =>
			{
				c.RouteTemplate = "api-docs/{documentName}/swagger.json";
			});
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/api-docs/v1/swagger.json", "My API V1");
			});

			app.UseAuthorization();

			app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
