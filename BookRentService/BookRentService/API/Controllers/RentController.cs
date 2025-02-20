using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookRentService.Application.DTO.BookRentDTO;
using BookRentService.Application.Queries;
using BookRentService.Application.Exceptions;
using BookRentService.Application.Commands;

namespace RentRentService.API.Controllers
{
    [ApiController]
    [Route("api/rents")]
    public class RentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить список всех аренд
        /// </summary>
        /// <returns>Список аренд</returns>
        [HttpGet]
        public async Task<ActionResult<List<BookRentForListDto>>> GetRents()
        {
            try
            {
                var query = new GetRentsQuery();
                var rents = await _mediator.Send(query);
                return Ok(rents);
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[RentController.GetRents]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

        /// <summary>
        /// Получить аренду по ID
        /// </summary>
        /// <param name ="id">ID аренды</param>
        /// <returns>Данные аренды</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookRentDto>> GetRentById(int id)
        {
            try
            {
                var rent = await _mediator.Send(new GetRentByIdQuery(id));
                if (rent == null) return NotFound();
                return Ok(rent);
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[RentController.GetRentById]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

        /// <summary>
        /// Создать аренду
        /// </summary>
        /// <param name ="command">Данные для создания аренды</param>
        /// <returns>Id созданной аренды</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateRent([FromBody] CreateRentCommand command)
        {
            try
            {
                var rentId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetRentById), new { id = rentId }, rentId);
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[RentController.CreateRent]: " + e.Message);
                return StatusCode(500, new { message = "Неизвестная ошибка!" });
            }
        }
        
        /// <summary>
        /// Обновить аренду
        /// </summary>
        /// <param name ="id">ID аренды</param>
        /// <param name ="command">Данные для обновления аренды</param>
        /// <returns>Результат</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRent(int id, [FromBody] UpdateRentCommand command)
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
                Console.WriteLine("[RentController.UpdateRent]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }

        /// <summary>
        /// Удалить аренду
        /// </summary>
        /// <param name ="id">ID аренды</param>
        /// <returns>Результат</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRent(int id)
        {
            try
            {
                var success = await _mediator.Send(new DeleteRentCommand(id));
                if (!success) return NotFound();

                return NoContent();
            }
            catch (AppException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("[RentController.DeleteRent]: " + e.Message);
                return StatusCode(500, new { messge = "Неизвестная ошибка!" });
            }
        }
    }
}