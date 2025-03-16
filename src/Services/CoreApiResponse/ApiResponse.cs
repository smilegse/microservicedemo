
namespace CoreApiResponse
{
    internal class ApiResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public dynamic Data { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(string message)
        {
            Message = message;
        }

        public ApiResponse(string message, dynamic data)
            : this(message)
        {
            Data = (object)data;
        }
    }
}
