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
    public class CarModelsController : ControllerBase
    {
        private readonly ICarModelControl _carModelControl;

        public CarModelsController(ICarModelControl carModelControl)
        {
            _carModelControl = carModelControl;
        }

        // GET https://localhost:7134/api/carmodels
        [HttpGet]
        public ActionResult<List<Car>> GetCarModels()
        {
            var allCarModels = _carModelControl.GetAllCarModels();

            if (allCarModels == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: List of carModels is null. " +
                                  "A bad GET request was made.");
            }

            if (allCarModels.Count == 0)
            {
                // Return 404: No carModels found
                return NotFound("ERROR: No carModels found in the database.");
            }

            // Return 200: OK
            return Ok(allCarModels);
        }

        // GET https://localhost:7134/api/carmodels/ID
        [HttpGet("{ID:int}")]
        public ActionResult<Car> GetCarModel(int ID)
        {
            var foundCarModel = _carModelControl.GetCarModelByID(ID);

            if (foundCarModel == null)
            {
                // Return 404: No carModel found, null
                return NotFound($"CarModel with ID '{ID}' not found.");
            }

            // Return 200: OK
            return Ok(foundCarModel);
        }

        // POST https://localhost:7134/api/carmodels
        [HttpPost]
        public ActionResult<Car> CreateCarModel([FromBody] Car newCarModel)
        {
            if (newCarModel == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad CarModel request body.");
            }

            var wasCarModelCreated = _carModelControl.AddCarModel(newCarModel);

            if (wasCarModelCreated)
            {
                // Return 201: Created CarModel
                return CreatedAtAction(
                    nameof(GetCarModel),
                    new { ID = newCarModel.ID },
                    newCarModel);
            }

            // Return 409: Conflict if failure when creating carModel (e.g., duplicate VINNumber)
            return Conflict($"ERROR: CarModel with ID '{newCarModel.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // POST https://localhost:7134/api/dto/carmodels
        [HttpPost("dto")]
        public ActionResult<CarModelViewModel> CreateCarModelDTO([FromBody] CarModelViewModel newCarModel)
        {
            if (newCarModel == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad CarModel request body.");
            }

            var wasCarModelCreated = _carModelControl.AddCarModelDTO(newCarModel);

            if (wasCarModelCreated)
            {
                // Return 201: Created CarModel
                return CreatedAtAction(
                    nameof(GetCarModel),
                    new { ID = newCarModel.ID },
                    newCarModel);
            }

            // Return 409: Conflict if failure when creating carModel (e.g., duplicate VINNumber)
            return Conflict($"ERROR: CarModel with ID '{newCarModel.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT https://localhost:7134/api/dto/carmodels/ID
        [HttpPut("{ID:int}")]
        public IActionResult UpdateCustomer(int ID, [FromBody] Car updatedCarModel)
        {
            if (updatedCarModel == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad CarModel request body.");
            }

            var existingCarModel = _carModelControl.GetCarModelByID(ID);

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

            var wasCarModelUpdated = _carModelControl.UpdateCarModel(updatedCarModel);

            if (!wasCarModelUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update CarModel with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // PUT https://localhost:7134/api/dto/carmodels/dto/ID
        [HttpPut("dto/{ID:int}")]
        public IActionResult UpdateCustomerDTO(int ID, [FromBody] CarModelViewModel updatedCarModel)
        {
            if (updatedCarModel == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad CarModel request body.");
            }

            var existingCarModel = _carModelControl.GetCarModelByID(ID);

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

            var wasCarModelUpdated = _carModelControl.UpdateCarModelDTO(updatedCarModel);

            if (!wasCarModelUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update CarModel with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // DELETE: https://localhost:7134/api/carmodels/ID
        [HttpDelete("{ID:int}")]
        public IActionResult DeleteCustomer(int ID)
        {
            var foundCarModel = _carModelControl.GetCarModelByID(ID);

            if (foundCarModel == null)
            {
                // Return 404: no existing carModel found
                return NotFound($"No existing CarModel with ID '{foundCarModel.ID}' found.");
            }

            var wasCarModelRemoved = _carModelControl.DeleteCarModel(ID);

            if (!wasCarModelRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete CarModel with ID '{foundCarModel.ID}' from database.");
            }

            // Return 204: No content (Successful deletion)
            return NoContent();
        }
    }
}
