using BisleriumCafeBackend.enums;

namespace BisleriumCafeBackend.pojo
{

    public class GlobalApiResponse
    {
        //public enums.ResponseStatus Status { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        //public void SetResponse(string message, enums.ResponseStatus status, object data)
        public void SetResponse(string message, int status, object data)
        {
            Message = message;
            Status = status;
            Data = data;
        }
    }

    public enum ResponseStatus
    {
        // Add your status values here if needed
    }

}
