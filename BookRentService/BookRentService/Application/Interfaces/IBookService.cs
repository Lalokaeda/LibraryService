using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.DTO.BookExemplarDTO;

namespace BookRentService.Application.Interfaces
{
    public interface IBookService
    {
        Task<List<BookExemplarDto>> GetBooksByIdsAsync(List<int> booksId);
    }
}