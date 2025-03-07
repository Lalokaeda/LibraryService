using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryService.Application.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode{get;}

        public AppException(string? message, int statusCode) : base(message)
        {
            StatusCode=statusCode;
        }
    }
}