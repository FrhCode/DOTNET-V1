namespace MagicVilla;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
	private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

	public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
	{
		_logger = logger;
	}


	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (NotImplementedException Ex)
		{
			_logger.LogError(Ex, Ex.Message);
			context.Response.StatusCode = 501;
			await context.Response.WriteAsJsonAsync(new { Message = "Not Implemented" });
		}
		catch (ResourceNotFoundException Ex)
		{
			_logger.LogError(Ex, Ex.Message);
			context.Response.StatusCode = 404;
			await context.Response.WriteAsJsonAsync(new { Message = Ex.Message });
		}
		catch (DuplicateResource Ex)
		{
			_logger.LogError(Ex, Ex.Message);
			context.Response.StatusCode = 409;
			await context.Response.WriteAsJsonAsync(new { Message = "Duplicate Resource" });
		}
		catch (Exception Ex)
		{
			_logger.LogError(Ex, Ex.Message);
			context.Response.StatusCode = 500;
			await context.Response.WriteAsJsonAsync(new { Message = "Internal Server Error" });
		}
	}
}
