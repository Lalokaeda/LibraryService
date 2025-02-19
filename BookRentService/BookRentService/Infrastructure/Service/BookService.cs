using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.DTO.BookExemplarDTO;
using BookRentService.Application.Interfaces;

namespace BookRentService.Infrastructure.Service
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient=httpClient;
        }

        public async Task<List<BookExemplarDto>> GetBooksByIdsAsync(List<int> booksId)
        {
            try
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/books/bookExemplars/details", booksId);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<BookExemplarDto>>() ?? new List<BookExemplarDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BookService.GetBooksByIdAsync]: {ex.Message}");
            return new List<BookExemplarDto>();
        }
        }
    }
}