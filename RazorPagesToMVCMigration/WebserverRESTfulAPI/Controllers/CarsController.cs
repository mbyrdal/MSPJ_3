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
        public ActionResult<List<Car>> GetCars()
        {
            var allCars = _carControl.GetAllCars();

            if (allCars == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: List of cars is null. " +
                                  "A bad GET request was made.");
            }

            if (allCars.Count == 0)
            {
                // Return 404: No cars found
                return NotFound("ERROR: No cars found in the database.");
            }

            // Return 200: OK
            return Ok(allCars);
        }

        // GET https://localhost:7134/api/cars/ID
        [HttpGet("{ID:int}")]
        public ActionResult<Car> GetCar(int ID)
        {
            var foundCar = _carControl.GetCarByID(ID);

            if (foundCar == null)
            {
                // Return 404: No carModel found, null
                return NotFound($"Car with ID '{ID}' not found.");
            }

            // Return 200: OK
            return Ok(foundCar);
        }

        // POST https://localhost:7134/api/cars
        [HttpPost]
        public ActionResult<Car> CreateCar([FromBody] Car newCar)
        {
            if (newCar == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Car request body.");
            }

            var wasCarCreated = _carControl.AddCar(newCar);

            if (wasCarCreated)
            {
                // Return 201: Created Car
                return CreatedAtAction(
                    nameof(GetCar),
                    new { ID = newCar.ID },
                    newCar);
            }

            // Return 409: Conflict if failure when creating car (e.g., duplicate VINNumber)
            return Conflict($"ERROR: Car with ID '{newCar.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // POST https://localhost:7134/api/dto/cars
        [HttpPost("dto")]
        public ActionResult<CarViewModel> CreateCarDTO([FromBody] CarViewModel newCar)
        {
            if (newCar == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Car request body.");
            }

            var wasCarCreated = _carControl.AddCarDTO(newCar);

            if (wasCarCreated)
            {
                // Return 201: Created Car
                return CreatedAtAction(
                    nameof(GetCar),
                    new { ID = newCar.ID },
                    newCar);
            }

            // Return 409: Conflict if failure when creating car (e.g., duplicate VINNumber)
            return Conflict($"ERROR: Car with ID '{newCar.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT https://localhost:7134/api/dto/cars/ID
        [HttpPut("{ID:int}")]
        public IActionResult UpdateCar(int ID, [FromBody] Car updatedCar)
        {
            if (updatedCar == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Car request body.");
            }

            var existingCar = _carControl.GetCarByID(ID);

            if (existingCar == null)
            {
                // Return 404: no existing car found
                return NotFound($"No existing Car with ID '{existingCar.ID}' found.");
            }

            if (existingCar.ID != updatedCar.ID)
            {
                // Return 409: IDs of existing car and response body car do not match.
                return Conflict($"Found Car with ID '{existingCar.ID}' does not match ID in request body '{updatedCar.ID}'.");
            }

            var wasCarUpdated = _carControl.UpdateCar(updatedCar);

            if (!wasCarUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update Car with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // PUT https://localhost:7134/api/dto/cars/dto/ID
        [HttpPut("dto/{ID:int}")]
        public IActionResult UpdateCarDTO(int ID, [FromBody] CarViewModel updatedCar)
        {
            if (updatedCar == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Car request body.");
            }

            var existingCar = _carControl.GetCarByID(ID);

            if (existingCar == null)
            {
                // Return 404: no existing car found
                return NotFound($"No existing Car with ID '{existingCar.ID}' and OEM '{existingCar.VINNumber}' found.");
            }

            if (existingCar.ID != updatedCar.ID)
            {
                // Return 409: IDs of existing car and response body car do not match.
                return Conflict($"Found Car with ID '{existingCar.ID}' does not match ID in request body '{updatedCar.ID}'.");
            }

            var wasCarUpdated = _carControl.UpdateCarDTO(updatedCar);

            if (!wasCarUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update Car with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // DELETE: https://localhost:7134/api/cars/ID
        [HttpDelete("{ID:int}")]
        public IActionResult DeleteCar(int ID)
        {
            var foundCar = _carControl.GetCarByID(ID);

            if (foundCar == null)
            {
                // Return 404: no existing car found
                return NotFound($"No existing Car with ID '{foundCar.ID}' found.");
            }

            var wasCarRemoved = _carControl.DeleteCar(ID);

            if (!wasCarRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete Car with ID '{foundCar.ID}' from database.");
            }

            // Return 204: No content (Successful deletion)
            return NoContent();
        }
    }
}
