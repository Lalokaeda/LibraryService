using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Application.Exceptions
{
    public class NotFoundException: AppException
    {
        public NotFoundException(string? message) : base(message, StatusCodes.Status404NotFound)
        {
        }
        
    }
}