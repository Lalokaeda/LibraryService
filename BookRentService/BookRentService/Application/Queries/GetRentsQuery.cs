using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.DTO.BookRentDTO;
using MediatR;

namespace BookRentService.Application.Queries
{
    public class GetRentsQuery : IRequest<List<BookRentForListDto>>
    {
        
    }
}