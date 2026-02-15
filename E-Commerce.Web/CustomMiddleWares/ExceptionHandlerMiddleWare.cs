using E_Commerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.CustomMiddleWares
{
	public class ExceptionHandlerMiddleWare
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlerMiddleWare> _logger;

		public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next.Invoke(httpContext);
				await HandleNotFoundEndPointAsync(httpContext);
			}
			catch (Exception ex)
			{
				// Logging
				_logger.LogError(ex, "An unhandled exception occurred while processing the request.");	
				// Return a generic error response
				var Problem = new ProblemDetails()
				{
					Title = "Error While Processing HTTP Request",
					Detail = ex.Message,
					Instance = httpContext.Request.Path,
					Status = ex switch
					{
						NotFoundException => StatusCodes.Status404NotFound,
						_ => StatusCodes.Status500InternalServerError
					}
				};
				httpContext.Response.StatusCode = Problem.Status.Value;
				await httpContext.Response.WriteAsJsonAsync(Problem);
			}
		}

		private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
		{
			if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
			{
				// Log the error response
				var Response = new ProblemDetails()
				{
					Title = "Error Whill Processing The HTTP Request - EndPoint Not Found",
					Detail = $"EndPoint {httpContext.Request.Path} was not found.",
					Status = StatusCodes.Status404NotFound,
					Instance = httpContext.Request.Path
				};
				await httpContext.Response.WriteAsJsonAsync(Response);
			}
		}
	}
}
