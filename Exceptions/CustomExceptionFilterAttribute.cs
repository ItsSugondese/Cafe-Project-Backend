    using BisleriumCafeBackend.pojo;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
namespace BisleriumCafeBackend.Exceptions
{

    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            var apiError = new ApiError
            {
                Status = (int) enums.ResponseStatus.Fail,
                HttpCode = 500, // You may customize this as needed
                Message = context.Exception.Message,
                Errors = new List<string> { context.Exception.Message }
            };

            context.Result = new ObjectResult(apiError)
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
            };
        }
    }

}
