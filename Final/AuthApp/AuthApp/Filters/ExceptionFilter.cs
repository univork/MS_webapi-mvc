using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthApp.Filters
{
    public class ExceptionFilter: IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext exceptionContext)
        {
            var controllerName = exceptionContext.RouteData.Values["controller"];
            var actionName = exceptionContext.RouteData.Values["action"];

            _logger.Log(LogLevel.Error, $"Exception in {controllerName}.{actionName} - Message: {exceptionContext.Exception.Message}");
            exceptionContext.Result = new StatusCodeResult(500);
        }
    }
}
