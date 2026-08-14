using GarageManager.Dtos;
using GarageManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace GarageManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceOrdersController : ControllerBase
    {
        private readonly ServiceOrderService _service;

        public ServiceOrdersController(ServiceOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceOrderDto>> Create(
                     CreateServiceOrderDto dto)
        {
            var order = await _service.Create(dto);

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceOrderDto>>> GetAll()
        {
            var orders = await _service.GetAll();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceOrderDto>> GetById(int id)
        {
            var order = await _service.GetById(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceOrderDto>> Update(
                     int id, UpdateServiceOrderDto dto)
        {
            var order = await _service.Update(id, dto);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("statistics/{year}/{month}")]
        public async Task<ActionResult<MonthlyStatisticsDto>> GetMonthlyStatistics(
            int year,
            int month)
        {
            if (month < 1 || month > 12)
                return BadRequest("Month must be between 1 and 12.");

            var statistics =
                await _service.GetMonthlyStatistics(year, month);

            return Ok(statistics);
        }
    }
}
