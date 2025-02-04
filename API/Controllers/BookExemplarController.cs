using LibraryService.Application.Commands;
using LibraryService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.API.Controllers
{
    [ApiController]
    [Route("api/books/bookExemplars")]
    public class BookExemplarController : Controller
    {
        private readonly IMediator _mediator;

        public BookExemplarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateBookExemplar([FromBody] CreateBookExemplarCommand command)
        {
            var bookExemplarId = await _mediator.Send(command);

            return Ok(bookExemplarId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookExemplar(int id, [FromBody] UpdateBookExemplarCommand command)
        {
            if (id != command.Id) return BadRequest("ID в URL и теле запроса должны совпадать");

            var success = await _mediator.Send(command);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookExemplar(int id)
        {
            var success = await _mediator.Send(new DeleteBookExemplarCommand(id));
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpGet("sorted")]
        public async Task<IActionResult> GetSortedBookExemplars(
                                                                [FromQuery] string sortBy = "Shelf",
                                                                [FromQuery] bool descending = false)
        {
            var query = new GetBookExemplarsSortedQuery(sortBy, descending);
            var exemplars = await _mediator.Send(query);
            return Ok(exemplars);
        }
    }
}