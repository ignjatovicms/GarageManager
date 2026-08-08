using GarageManager.Models;
using GarageManager.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GarageManager.Dtos;
using GarageManager.Services;


namespace GarageManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _carService.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _carService.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarDto dto)
        {
            var result = await _carService.Create(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _carService.Delete(id);

            if (!ok)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCarDto dto)
        {
            var ok = await _carService.Update(id, dto);

            if (!ok)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string brand)
        {
            var results = await _carService.Search(brand);

            return Ok(results);
        }
    }
}
