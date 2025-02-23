using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Application.Exceptions
{
    public class ValidationException : AppException
    {
        public ValidationException(string? message) : base(message, StatusCodes.Status404NotFound)
        {
        }
    }
}