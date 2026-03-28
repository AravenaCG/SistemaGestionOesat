using System;

namespace BlazorAppOrquesta
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string? Response { get; set; }

        public ApiException()
        {
        }

        public ApiException(string message)
            : base(message)
        {
        }

        public ApiException(string message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public ApiException(string message, int statusCode, string? response)
            : base(message)
        {
            StatusCode = statusCode;
            Response = response;
        }

        public ApiException(string message, int statusCode, string? response, Exception? innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Response = response;
        }
    }
}