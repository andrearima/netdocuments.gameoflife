using Microsoft.AspNetCore.Mvc.Filters;

namespace NetDocuments.GameOfLife.Api.Handler;

public class ExceptionFilter : IAsyncExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger;
    }

    public Task OnExceptionAsync(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "An error occurred");
        context.HttpContext.Response.StatusCode = 500;
        context.ExceptionHandled = true;
        return Task.CompletedTask;
    }
}
