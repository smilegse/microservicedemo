﻿using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiResponse
{
    internal class HttpCustomResponse : IActionResult
    {
        public HttpRequest Request { get; }

        public HttpStatusCode Code { get; }

        public ApiResponse Response { get; }

        public HttpCustomResponse(HttpRequest request)
            : this(request, HttpStatusCode.OK)
        {
        }

        public HttpCustomResponse(HttpRequest request, HttpStatusCode code)
            : this(request, code, null)
        {
        }

        public HttpCustomResponse(HttpRequest request, HttpStatusCode code, ApiResponse response)
        {
            Request = request;
            Code = code;
            Response = response;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult obg = new ObjectResult(Response)
            {
                StatusCode = (int)Code
            };
            await obg.ExecuteResultAsync(context);
        }
    }
}
