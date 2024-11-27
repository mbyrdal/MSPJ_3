using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic;
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
        public ActionResult<Car> CreateCarModel([FromBody] Car newCarModel)
        {
            if (newCarModel == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad CarModel request body.");
            }

            var wasCarModelCreated = _carControl.AddCar(newCarModel);

            if (wasCarModelCreated)
            {
                // Return 201: Created CarModel
                return CreatedAtAction(
                    nameof(GetCar),
                    new { ID = newCarModel.ID },
                    newCarModel);
            }

            // Return 409: Conflict if failure when creating carModel (e.g., duplicate VINNumber)
            return Conflict($"ERROR: CarModel with ID '{newCarModel.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // POST https://localhost:7134/api/dto/cars
        [HttpPost("dto")]
        public ActionResult<CarViewModel> CreateCarModelDTO([FromBody] CarViewModel newCarModel)
        {
            if (newCarModel == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad CarModel request body.");
            }

            var wasCarModelCreated = _carControl.AddCarDTO(newCarModel);

            if (wasCarModelCreated)
            {
                // Return 201: Created CarModel
                return CreatedAtAction(
                    nameof(GetCar),
                    new { ID = newCarModel.ID },
                    newCarModel);
            }

            // Return 409: Conflict if failure when creating carModel (e.g., duplicate VINNumber)
            return Conflict($"ERROR: CarModel with ID '{newCarModel.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT https://localhost:7134/api/dto/cars/ID
        [HttpPut("{ID:int}")]
        public IActionResult UpdateCustomer(int ID, [FromBody] Car updatedCarModel)
        {
            if (updatedCarModel == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad CarModel request body.");
            }

            var existingCarModel = _carControl.GetCarByID(ID);

            if (existingCarModel == null)
            {
                // Return 404: no existing carModel found
                return NotFound($"No existing CarModel with ID '{existingCarModel.ID}' found.");
            }

            if (existingCarModel.ID != updatedCarModel.ID)
            {
                // Return 409: IDs of existing carModel and response body carModel do not match.
                return Conflict($"Found CarModel with ID '{existingCarModel.ID}' does not match ID in request body '{updatedCarModel.ID}'.");
            }

            var wasCarModelUpdated = _carControl.UpdateCar(updatedCarModel);

            if (!wasCarModelUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update CarModel with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // PUT https://localhost:7134/api/dto/cars/dto/ID
        [HttpPut("dto/{ID:int}")]
        public IActionResult UpdateCustomerDTO(int ID, [FromBody] CarViewModel updatedCarModel)
        {
            if (updatedCarModel == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad CarModel request body.");
            }

            var existingCarModel = _carControl.GetCarByID(ID);

            if (existingCarModel == null)
            {
                // Return 404: no existing carModel found
                return NotFound($"No existing CarModel with ID '{existingCarModel.ID}' found.");
            }

            if (existingCarModel.ID != updatedCarModel.ID)
            {
                // Return 409: IDs of existing carModel and response body carModel do not match.
                return Conflict($"Found CarModel with ID '{existingCarModel.ID}' does not match ID in request body '{updatedCarModel.ID}'.");
            }

            var wasCarModelUpdated = _carControl.UpdateCarDTO(updatedCarModel);

            if (!wasCarModelUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update CarModel with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // DELETE: https://localhost:7134/api/cars/ID
        [HttpDelete("{ID:int}")]
        public IActionResult DeleteCustomer(int ID)
        {
            var foundCarModel = _carControl.GetCarByID(ID);

            if (foundCarModel == null)
            {
                // Return 404: no existing carModel found
                return NotFound($"No existing CarModel with ID '{foundCarModel.ID}' found.");
            }

            var wasCarModelRemoved = _carControl.DeleteCar(ID);

            if (!wasCarModelRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete CarModel with ID '{foundCarModel.ID}' from database.");
            }

            // Return 204: No content (Successful deletion)
            return NoContent();
        }
    }
}
