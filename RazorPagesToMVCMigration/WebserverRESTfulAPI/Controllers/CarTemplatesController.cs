using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.Models;

namespace ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarTemplatesController : ControllerBase
    {
        private readonly ICarTemplateControl _carTemplateControl;

        public CarTemplatesController(ICarTemplateControl carTemplateControl)
        {
            _carTemplateControl = carTemplateControl;
        }

        // GET https://localhost:7134/api/cartemplates
        [HttpGet]
        public ActionResult<List<CarTemplate>> GetCarTemplates()
        {
            var allCarTemplates = _carTemplateControl.GetAllCarTemplates();

            if (allCarTemplates == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: List of carTemplates is null. " +
                                  "A bad GET request was made.");
            }

            if (allCarTemplates.Count == 0)
            {
                // Return 404: No cars found
                return NotFound("ERROR: No carTemplates found in the database.");
            }

            // Return 200: OK
            return Ok(allCarTemplates);
        }

        // GET https://localhost:7134/api/cartemplates/ID
        [HttpGet("{ID:int}")]
        public ActionResult<CarTemplate> GetCarTemplate(int ID)
        {
            var foundCarTemplate = _carTemplateControl.GetCarTemplateByID(ID);

            if (foundCarTemplate == null)
            {
                // Return 404: No carModel found, null
                return NotFound($"CarTemplate with ID '{ID}' not found.");
            }

            // Return 200: OK
            return Ok(foundCarTemplate);
        }

        // POST https://localhost:7134/api/cartemplates
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
    }
}
