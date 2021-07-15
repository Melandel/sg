using System.Linq;
using System.Text.Json;
using BookingAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BookingAPI
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

			services.AddControllers()
				.ConfigureApiBehaviorOptions(o =>
				{
					o.InvalidModelStateResponseFactory = context =>
					{
						var dto = new CustomErrorDto
						{
							Error = context.ModelState.First().Value.Errors.First().ErrorMessage,
							StatusCode = 400,
							RequestId = context.HttpContext.TraceIdentifier
						};
						return new ObjectResult(dto) { StatusCode = dto.StatusCode };
					};
				});
			services.AddHttpContextAccessor();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookingAPI", Version = "v1" });
				c.SchemaFilter<IgnoreReadOnlySchemaFilter>();
			});

			services.AddSingleton<IBookingRepository, BookingRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMiddleware<ErrorHandlingMiddleware>();
			if (env.IsDevelopment())
			{
				//app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookingAPI v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

  // Catch-all middleware
  // Arriving here means no endpoint has been matched by router
  app.Use(next => async context =>
  {
    var dto = new CustomErrorDto()
    {
      Error = "The requested endpoint doesn't exist",
      StatusCode = 404,
      RequestId = context.TraceIdentifier
    };

    var result = new ObjectResult(dto){ StatusCode = dto.StatusCode };
    await context.Response.WriteAsync(JsonSerializer.Serialize(result));
  });
		}
	}
}
