using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Application.Exceptions
{
    public class DataBaseException : AppException
    {
        public DataBaseException(string? message) : base(message, StatusCodes.Status500InternalServerError)
        {
        }
    }
}