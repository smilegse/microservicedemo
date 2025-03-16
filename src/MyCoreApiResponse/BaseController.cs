using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreApiResponse
{
    public class BaseController : ControllerBase
    {
        protected IActionResult CustomResult(string message, HttpStatusCode code = HttpStatusCode.OK)
        {
            ApiResponse apiResponse = new ApiResponse(message);
            apiResponse.StatusCode = (int)code;
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(code);
            return new HttpCustomResponse(base.Request, code, apiResponse);
        }

        protected IActionResult CustomResult(object data, HttpStatusCode code = HttpStatusCode.OK)
        {
            ApiResponse apiResponse = new ApiResponse("", data);
            apiResponse.StatusCode = (int)code;
            apiResponse.Data = data;
            return new HttpCustomResponse(base.Request, code, apiResponse);
        }

        protected IActionResult CustomResult(string message, object data, HttpStatusCode code = HttpStatusCode.OK)
        {
            ApiResponse apiResponse = new ApiResponse(message, data);
            apiResponse.StatusCode = (int)code;
            apiResponse.Data = data;
            return new HttpCustomResponse(base.Request, code, apiResponse);
        }

        protected IActionResult CustomResult(HttpStatusCode code = HttpStatusCode.OK)
        {
            return new HttpCustomResponse(base.Request, code, null);
        }
    }
}
