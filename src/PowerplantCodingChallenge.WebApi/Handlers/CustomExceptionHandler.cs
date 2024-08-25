using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PowerplantCodingChallenge.WebApi.Handlers;

public class CustomExceptionHandler : IExceptionHandler
{
  private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

  public CustomExceptionHandler()
  {
    _exceptionHandlers = new()
            {
                { typeof(Exception), HandleHandleGlobalException },
                { typeof(UnsupportedPowerPlantTypeException), HandleUnsupportedPowerPlantTypeException },
            };
  }

  public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
  {
    var exceptionType = exception.GetType();

    if (_exceptionHandlers.ContainsKey(exceptionType))
    {
      await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
      return true;
    }

    return false;
  }

  private async Task HandleUnsupportedPowerPlantTypeException(HttpContext httpContext, Exception exception)
  {
    var unsupportedPowerPlantTypeException = (UnsupportedPowerPlantTypeException)exception;

    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

    await httpContext.Response.WriteAsJsonAsync(new ValidationProblemDetails()
    {
      Status = StatusCodes.Status400BadRequest,
      Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      Detail = unsupportedPowerPlantTypeException.Message
    });
  }

  private static async Task HandleHandleGlobalException(HttpContext httpContext, Exception exception)
  {
    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

    await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
    {
      Status = StatusCodes.Status500InternalServerError,
      Title = "Service unavailable!",
      Detail = exception.Message,
      Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
    });
  }
}
