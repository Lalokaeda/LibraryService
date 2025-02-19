using LibraryService.Application.Commands;
using LibraryService.Application.Exceptions;
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

        /// <summary>
        /// Создать экземпляр книги
        /// </summary>
        /// <param name ="command">Данные для создания экземпляра книги</param>
        /// <returns>Id созданного экземпляра книги</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateBookExemplar([FromBody] CreateBookExemplarCommand command)
        {
            try
            {
                var bookExemplarId = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetBookExemplarById), new { id = bookExemplarId }, bookExemplarId);
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarController.CreateBookExemplar]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

        /// <summary>
        /// Обновить экземпляр книги
        /// </summary>
        /// <param name ="id">ID экземпляра книги</param>
        /// <param name ="command">Данные для обновления экземпляра книги</param>
        /// <returns>Результат</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookExemplar(int id, [FromBody] UpdateBookExemplarCommand command)
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
                Console.WriteLine("[BookExemplarController.UpdateBookExemplar]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

        /// <summary>
        /// Удалить экземпляр книги
        /// </summary>
        /// <param name ="id">ID экземпляра книги</param>
        /// <returns>Результат</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookExemplar(int id)
        {
            try
            {
                var success = await _mediator.Send(new DeleteBookExemplarCommand(id));
                if (!success) return NotFound();

                return NoContent();
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarController.DaleteBookExemplar]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

        /// <summary>
        /// Получить список всех экземпляров книг с сортировкой
        /// </summary>
        /// <param name ="sortBy">Условие для сортировки. По умолчанию "Shelf". Возможные варианты: "Shelf" (полка), "DateAdded" (дата добавления)</param>
        /// <param name ="descending">Порядк сортировки: true - по убыванию, false - по возрастанию. По умолчанию false</param>
        /// <returns>Отсортированный список экземпляров книг</returns>
        [HttpGet("sorted")]
        public async Task<IActionResult> GetSortedBookExemplars(
                                                                [FromQuery] string sortBy = "Shelf",
                                                                [FromQuery] bool descending = false)
        {
            try
            {
                var query = new GetBookExemplarsSortedQuery(sortBy, descending);
                var exemplars = await _mediator.Send(query);
                return Ok(exemplars);
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarController.GetSortedBookExemplars]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

        /// <summary>
        /// Получить экземпляр книги по ID
        /// </summary>
        /// <param name ="id">ID экземпляра книги</param>
        /// <returns>Данные экземпляра книги</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookExemplarById(int id)
        {
            try
            {
                var exemplar = await _mediator.Send(new GetBookExemplarByIdQuery(id));
                if (exemplar==null) return NotFound();
                return Ok(exemplar);
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarController.GetBookExemplarById]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

         /// <summary>
        /// Получить экземпляры книги по ID
        /// </summary>
        /// <param name ="ids">ID экземпляров книги</param>
        /// <returns>Данные экземпляров книги</returns>
        [HttpPost("details")]
        public async Task<IActionResult> GetBookExemplarsById([FromBody] List<int> ids)
        {
            try
            {
                var exemplars = await _mediator.Send(new GetBookExemplarsByIdQuery(ids));
                if (exemplars==null || exemplars.Count==0) return NotFound();
                return Ok(exemplars);
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarController.GetBookExemplarsById]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }
    }
}