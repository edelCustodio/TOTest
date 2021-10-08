using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOTest.Api.Infraestructure;
using TOTest.Api.Models;
using TOTest.Api.Services;

namespace TOTest.Api
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
			services.AddControllers(mvcOptions =>
				mvcOptions.EnableEndpointRouting = false);
			services.AddMemoryCache();
			services.AddOData();
			services.AddODataQueryFilter();

			services.AddHttpClient();
			services.AddScoped<IImageService, ImageService>();
			services.AddScoped<IHttpClient, HttpClient>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseMvc(routeBuilder =>
			{
				routeBuilder.EnableDependencyInjection();
				routeBuilder.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
				routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
			});

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		IEdmModel GetEdmModel()
		{
			var odataBuilder = new ODataConventionModelBuilder();
			odataBuilder.EntitySet<ImageViewModel>("Images");

			return odataBuilder.GetEdmModel();
		}
	}
}
