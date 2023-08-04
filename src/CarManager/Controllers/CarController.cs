using CarManager.DTOs;
using CarManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarProvider _carProvider;

        public CarController(ILogger<CarController> logger, ICarProvider carProvider)
        {
            _logger = logger;
            _carProvider = carProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CarCreateDto createDto)
        {
            var id = await _carProvider.CreateCar(createDto);
            return Ok(id);
        }

        [HttpGet("")]
        public async Task<IActionResult> Vars()
        {
            var cars = await _carProvider.GetCars();
            return Ok(cars);
        }
    }
}