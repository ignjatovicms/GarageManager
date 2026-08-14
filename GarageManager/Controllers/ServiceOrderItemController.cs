using GarageManager.Dtos;
using GarageManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace GarageManager.Controllers
{
    [ApiController]
    [Route("api/ServiceOrders/{serviceOrderId}/items")]
    public class ServiceOrderItemController : ControllerBase
    {
        private readonly ServiceOrderItemService _service;

        public ServiceOrderItemController(ServiceOrderItemService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceOrderItemDto>> Create(
            int serviceOrderId,
            CreateServiceOrderItemDto dto)
        {
            var item = await _service.Create(serviceOrderId, dto);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceOrderItemDto>>> GetAll(
            int serviceOrderId)
        {
            var items = await _service.GetAll(serviceOrderId);

            return Ok(items);
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<ServiceOrderItemDto>> GetById(
            int serviceOrderId,
            int itemId)
        {
            var item = await _service.GetById(serviceOrderId, itemId);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPut("{itemId}")]
        public async Task<ActionResult<ServiceOrderItemDto>> Update(
            int serviceOrderId,
            int itemId,
            CreateServiceOrderItemDto dto)
        {
            var item = await _service.Update(serviceOrderId, itemId, dto);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> Delete(
            int serviceOrderId,
            int itemId)
        {
            var deleted = await _service.Delete(serviceOrderId, itemId);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
