using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarControl _carControl;

        public CarsController(ICarControl carControl)
        {
            _carControl = carControl;
        }

        // GET https://localhost:7134/api/cars
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            var allCars = await _carControl.GetAllCarsAsync();

            if (allCars == null)
            {
                return BadRequest("ERROR: List of cars is null. A bad GET request was made.");
            }

            if (allCars.Count == 0)
            {
                return NotFound("ERROR: No cars found in the database.");
            }

            return Ok(allCars);
        }

        // GET https://localhost:7134/api/cars/ID
        [HttpGet("{ID:int}")]
        public async Task<ActionResult<Car>> GetCar(int ID)
        {
            var foundCar = await _carControl.GetCarByIDAsync(ID);

            if (foundCar == null)
            {
                return NotFound($"Car with ID '{ID}' not found.");
            }

            return Ok(foundCar);
        }

        // POST https://localhost:7134/api/cars
        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar([FromBody] Car newCar)
        {
            if (newCar == null)
            {
                return BadRequest("ERROR: Bad Car request body.");
            }

            var wasCarCreated = await _carControl.AddCarAsync(newCar);

            if (wasCarCreated)
            {
                return CreatedAtAction(
                    nameof(GetCar),
                    new { ID = newCar.ID },
                    newCar);
            }

            return Conflict($"ERROR: Car with ID '{newCar.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // POST https://localhost:7134/api/dto/cars
        [HttpPost("dto")]
        public async Task<ActionResult<CarViewModel>> CreateCarDTO([FromBody] CarViewModel newCar)
        {
            if (newCar == null)
            {
                return BadRequest("ERROR: Bad Car request body.");
            }

            var wasCarCreated = await _carControl.AddCarDTOAsync(newCar);

            if (wasCarCreated)
            {
                return CreatedAtAction(
                    nameof(GetCar),
                    new { ID = newCar.ID },
                    newCar);
            }

            return Conflict($"ERROR: Car with ID '{newCar.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT https://localhost:7134/api/dto/cars/ID
        [HttpPut("{ID:int}")]
        public async Task<IActionResult> UpdateCar(int ID, [FromBody] Car updatedCar)
        {
            if (updatedCar == null)
            {
                return BadRequest("ERROR: Bad Car request body.");
            }

            var existingCar = await _carControl.GetCarByIDAsync(ID);

            if (existingCar == null)
            {
                return NotFound($"No existing Car with ID '{ID}' found.");
            }

            if (existingCar.ID != updatedCar.ID)
            {
                return Conflict($"Found Car with ID '{existingCar.ID}' does not match ID in request body '{updatedCar.ID}'.");
            }

            var wasCarUpdated = await _carControl.UpdateCarAsync(updatedCar);

            if (!wasCarUpdated)
            {
                return StatusCode(500, $"ERROR: Unable to update Car with ID '{ID}' in the database.");
            }

            return NoContent();
        }

        // PUT https://localhost:7134/api/dto/cars/dto/ID
        [HttpPut("dto/{ID:int}")]
        public async Task<IActionResult> UpdateCarDTO(int ID, [FromBody] CarViewModel updatedCar)
        {
            if (updatedCar == null)
            {
                return BadRequest("ERROR: Bad Car request body.");
            }

            var existingCar = await _carControl.GetCarByIDAsync(ID);

            if (existingCar == null)
            {
                return NotFound($"No existing Car with ID '{ID}' and OEM '{existingCar?.VINNumber}' found.");
            }

            if (existingCar.ID != updatedCar.ID)
            {
                return Conflict($"Found Car with ID '{existingCar.ID}' does not match ID in request body '{updatedCar.ID}'.");
            }

            var wasCarUpdated = await _carControl.UpdateCarDTOAsync(updatedCar);

            if (!wasCarUpdated)
            {
                return StatusCode(500, $"ERROR: Unable to update Car with ID '{ID}' in the database.");
            }

            return NoContent();
        }

        // DELETE: https://localhost:7134/api/cars/ID
        [HttpDelete("{ID:int}")]
        public async Task<IActionResult> DeleteCar(int ID)
        {
            var foundCar = await _carControl.GetCarByIDAsync(ID);

            if (foundCar == null)
            {
                return NotFound($"No existing Car with ID '{ID}' found.");
            }

            var wasCarRemoved = await _carControl.DeleteCarAsync(ID);

            if (!wasCarRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete Car with ID '{ID}' from database.");
            }

            return NoContent();
        }
    }
}
