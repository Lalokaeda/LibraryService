using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.DTO.BookRentDTO;
using MediatR;

namespace BookRentService.Application.Queries
{
    public class GetRentByIdQuery : IRequest<BookRentDto>
    {
        public int Id {get; set;}
        public GetRentByIdQuery(int id)
        {
            Id=id;
        }

    }
}