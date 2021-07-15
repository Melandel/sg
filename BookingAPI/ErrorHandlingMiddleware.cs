using BookingAPI.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BookingAPI
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;



		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}



		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (FunctionalException functionalException)
			{
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				context.Response.ContentType = "application/json";



				var problemDetails = new CustomErrorDto
				{
					Error = functionalException.Message,
					StatusCode = context.Response.StatusCode,
					RequestId = context.TraceIdentifier,
				};



				await context.Response.WriteAsJsonAsync(problemDetails);
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				context.Response.ContentType = "application/json";



				var problemDetails = new CustomErrorDto
				{
					Error = ex.Message,
					StatusCode = context.Response.StatusCode,
					RequestId = context.TraceIdentifier,
				};



				await context.Response.WriteAsJsonAsync(problemDetails);
			}
		}
	}
}
