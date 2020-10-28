using Businessmodel.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Filters
{
    public class ApiExceptionFilterAttribute : TypeFilterAttribute
    {
        public ApiExceptionFilterAttribute() : base(typeof(HandleExceptionPrivateAttribute))
        {
        }
    }
    class HandleExceptionPrivateAttribute : ExceptionFilterAttribute, IApiRequestHandler
    {
        private readonly ILogger _logger;

        public HandleExceptionPrivateAttribute(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("Exeption Filter");
        }

        public void LogInfo(string className, string methodName, string msg)
        {
            _logger.LogInformation("ClassName: {0} MethodName: {1} Message: {2}"
                , className, methodName, msg);
        }
        public void RaiseBusinessException(string className, string methodName, string msg)
        {
            _logger.LogError("ClassName: {0} MethodName: {1} Business Exception: {2}"
                , className, methodName, msg);
        }

        public override void OnException(ExceptionContext context)
        {
            ApiError apiError = null;
            if (context.Exception is ApiException)
            {
                var ex = context.Exception as ApiException;
                context.Exception = null;
                apiError = new ApiError(ex.Message);

                context.HttpContext.Response.StatusCode = ex.StatusCode;
                _logger.LogError("ApiException", apiError);
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;
                _logger.LogError("UnauthorizedAccessException", apiError);
            }
            else if (context.Exception is NotImplementedException)
            {
                apiError = new ApiError("Not Implemented");
                context.HttpContext.Response.StatusCode = 401;
                _logger.LogError("NotImplementedException", apiError);
            }
            else if (context.Exception is ArgumentNullException)
            {
                apiError = new ApiError(string.Format("Parameter: {0} is required, it cannot be empty", (context.Exception as ArgumentNullException).ParamName));
                context.HttpContext.Response.StatusCode = 401;
                _logger.LogError("ArgumentNullException", apiError);
            }
            else
            {
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
                apiError = new ApiError(msg);
                apiError.detail = stack;

                context.HttpContext.Response.StatusCode = 500;

                _logger.LogError("Exception", apiError);
            }
            context.Result = new JsonResult(apiError);

            base.OnException(context);

        }
    }
}
