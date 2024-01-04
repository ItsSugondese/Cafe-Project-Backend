using BisleriumCafeBackend.enums;
using BisleriumCafeBackend.pojo;

namespace BisleriumCafeBackend.Generics.Controller
{
    public class GenericController
    {
        protected readonly enums.ResponseStatus ApiResponseStatus = enums.ResponseStatus.Success;
        protected GlobalApiResponse SuccessResponse(string message, object data)
        {
            GlobalApiResponse globalApiResponse = new GlobalApiResponse
            {
                Status = (int) ApiResponseStatus,
                Message = message,
                Data = data
            };
            return globalApiResponse;
        }
    }
}
