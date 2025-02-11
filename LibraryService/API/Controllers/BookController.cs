using LibraryService.Application.Commands;
using Newtonsoft.Json;
using LibraryService.Application.DTO.BooksDTO;
using LibraryService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LibraryService.Application.Exceptions;

namespace LibraryService.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить список всех книг
        /// </summary>
        /// <param name ="searchQuery">Поиск по автору, названию, году публикации</param>
        /// <returns>Список книг</returns>
        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetBooks([FromQuery] string? searchQuery)
        {
            try
            {
                var query = new GetBooksQuery(searchQuery);
                var books = await _mediator.Send(query);
                return Ok(books);
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookController.GetBooks]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

        /// <summary>
        /// Получить книгу по ID
        /// </summary>
        /// <param name ="id">ID книги</param>
        /// <returns>Данные книги</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            try
            {
                var book = await _mediator.Send(new GetBookByIdQuery(id));
                if (book == null) return NotFound();
                return Ok(book);
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookController.GetBookById]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

        /// <summary>
        /// Создать книгу
        /// </summary>
        /// <param name ="command">Данные для создания книги</param>
        /// <returns>Id созданной книги</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateBook([FromBody] CreateBookCommand command)
        {
            try
            {
                var bookId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetBookById), new { id = bookId }, bookId);
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookController.CreateBook]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }
        
        /// <summary>
        /// Обновить книгу
        /// </summary>
        /// <param name ="id">ID книги</param>
        /// <param name ="command">Данные для обновления книги</param>
        /// <returns>Результат</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookCommand command)
        {
            try
            {
                if (id != command.Id) return BadRequest("ID в URL и теле запроса должны совпадать");

                var success = await _mediator.Send(command);
                if (!success) return NotFound();

                return NoContent();
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookController.UpdateBook]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

        /// <summary>
        /// Удалить книгу
        /// </summary>
        /// <param name ="id">ID книги</param>
        /// <returns>Результат</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var success = await _mediator.Send(new DeleteBookCommand(id));
                if (!success) return NotFound();

                return NoContent();
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookController.DeleteBook]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }
    }
}
