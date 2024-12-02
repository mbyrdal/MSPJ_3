using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using Microsoft.Extensions.Logging;

namespace ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarControl _carControl;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarControl carControl, ILogger<CarsController> logger)
        {
            _carControl = carControl;
            _logger = logger;
        }

        // GET https://localhost:7134/api/cars
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            try
            {
                var allCars = await _carControl.GetAllCarsAsync();

                if (allCars == null || allCars.Count == 0)
                {
                    return NotFound("No cars found in the database.");
                }

                return Ok(allCars);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all cars.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET https://localhost:7134/api/cars/ID
        [HttpGet("{ID:int}")]
        public async Task<ActionResult<Car>> GetCar(int ID)
        {
            try
            {
                var foundCar = await _carControl.GetCarByIDAsync(ID);

                if (foundCar == null)
                {
                    return NotFound($"Car with ID '{ID}' not found.");
                }

                return Ok(foundCar);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving car with ID: {ID}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST https://localhost:7134/api/cars
        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar([FromBody] Car newCar)
        {
            if (newCar == null)
            {
                return BadRequest("Invalid car request body.");
            }

            try
            {
                var wasCarCreated = await _carControl.AddCarAsync(newCar);

                if (wasCarCreated)
                {
                    return CreatedAtAction(nameof(GetCar), new { ID = newCar.ID }, newCar);
                }

                return Conflict($"Car with ID '{newCar.ID}' already exists or insertion failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a car.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // POST https://localhost:7134/api/dto/cars
        [HttpPost("dto")]
        public async Task<ActionResult<CarViewModel>> CreateCarDTO([FromBody] CarViewModel newCar)
        {
            if (newCar == null)
            {
                return BadRequest("Invalid car request body.");
            }

            try
            {
                var wasCarCreated = await _carControl.AddCarDTOAsync(newCar);

                if (wasCarCreated)
                {
                    return CreatedAtAction(nameof(GetCar), new { ID = newCar.ID }, newCar);
                }

                return Conflict($"Car with ID '{newCar.ID}' already exists or insertion failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a car DTO.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT https://localhost:7134/api/cars/ID
        [HttpPut("{ID:int}")]
        public async Task<IActionResult> UpdateCar(int ID, [FromBody] Car updatedCar)
        {
            if (updatedCar == null)
            {
                return BadRequest("Invalid car request body.");
            }

            if (ID != updatedCar.ID)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            try
            {
                var wasCarUpdated = await _carControl.UpdateCarAsync(updatedCar);

                if (wasCarUpdated)
                {
                    return NoContent();
                }

                return NotFound($"Car with ID '{ID}' does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating car with ID: {ID}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT https://localhost:7134/api/cars/dto/ID
        [HttpPut("dto/{ID:int}")]
        public async Task<IActionResult> UpdateCarDTO(int ID, [FromBody] CarViewModel updatedCar)
        {
            if (updatedCar == null)
            {
                return BadRequest("Invalid car request body.");
            }

            if (ID != updatedCar.ID)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            try
            {
                var wasCarUpdated = await _carControl.UpdateCarDTOAsync(updatedCar);

                if (wasCarUpdated)
                {
                    return NoContent();
                }

                return NotFound($"Car with ID '{ID}' does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating car DTO with ID: {ID}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // DELETE: https://localhost:7134/api/cars/ID
        [HttpDelete("{ID:int}")]
        public async Task<IActionResult> DeleteCar(int ID)
        {
            try
            {
                var wasCarDeleted = await _carControl.DeleteCarAsync(ID);

                if (wasCarDeleted)
                {
                    return NoContent();
                }

                return NotFound($"Car with ID '{ID}' does not exist.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting car with ID: {ID}.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
