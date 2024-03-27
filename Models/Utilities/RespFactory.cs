using System.Net;

namespace Server.Models.Utilities
{
    public static class RespFactory
    {
        public static APIResponse ReturnOk()
        {
            return new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };
        }

        public static APIResponse ReturnOk(object obj)
        {
            return new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = obj
            };
        }

        public static APIResponse ReturnBadRequest()
        {
            return new APIResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false
            };
        }

        public static APIResponse ReturnBadRequest(object obj)
        {
            return new APIResponse()
            {
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccess = false,
                Result = obj
            };
        }
    }
}
