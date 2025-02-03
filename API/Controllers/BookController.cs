using LibraryService.Application.Commands;
using Newtonsoft.Json;
using LibraryService.Application.DTO.BooksDTO;
using LibraryService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetBooks([FromQuery] GetBooksQuery query)
        {
            var books = await _mediator.Send(query);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBook([FromBody] CreateBookCommand command)
        {
            Console.WriteLine(JsonConvert.SerializeObject(command));
            var bookId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBookById), new { id = bookId }, bookId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookCommand command)
        {
            if (id != command.Id) return BadRequest("ID в URL и теле запроса должны совпадать");

            var success = await _mediator.Send(command);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var success = await _mediator.Send(new DeleteBookCommand(id));
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
