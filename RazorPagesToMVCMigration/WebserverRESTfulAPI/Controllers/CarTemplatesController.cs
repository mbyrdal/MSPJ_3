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
        public ActionResult<CarTemplate> CreateCarTemplate([FromBody] CarTemplate newCarTemplate)
        {
            if (newCarTemplate == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad CarTemplate request body.");
            }

            var wasCarTemplateCreated = _carTemplateControl.AddCarTemplate(newCarTemplate);

            if (wasCarTemplateCreated)
            {
                // Return 201: Created CarModel
                return CreatedAtAction(
                    nameof(GetCarTemplate),
                    new { ID = newCarTemplate.ID },
                    newCarTemplate);
            }

            // Return 409: Conflict if failure when creating carTemplate (e.g., duplicate ID)
            return Conflict($"ERROR: CarTemplate with ID '{newCarTemplate.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT https://localhost:7134/api/dto/cartemplates/ID
        [HttpPut("{ID:int}")]
        public IActionResult UpdateCarTemplate(int ID, [FromBody] CarTemplate updatedCarTemplate)
        {
            if (updatedCarTemplate == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad CarTemplate request body.");
            }

            var existingCarTemplate = _carTemplateControl.GetCarTemplateByID(ID);

            if (existingCarTemplate == null)
            {
                // Return 404: no existing carTemplate found
                return NotFound($"No existing Car with ID '{existingCarTemplate.ID}' found.");
            }

            if (existingCarTemplate.ID != updatedCarTemplate.ID)
            {
                // Return 409: IDs of existing carTemplate and response body car do not match.
                return Conflict($"Found CarTemplate with ID '{existingCarTemplate.ID}' does not match ID in request body '{updatedCarTemplate.ID}'.");
            }

            var wasCarTemplateUpdated = _carTemplateControl.UpdateCarTemplate(updatedCarTemplate);

            if (!wasCarTemplateUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update CarTemplate with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // DELETE: https://localhost:7134/api/cartemplates/ID
        [HttpDelete("{ID:int}")]
        public IActionResult DeleteCarTemplate(int ID)
        {
            var foundCarTemplate = _carTemplateControl.GetCarTemplateByID(ID);

            if (foundCarTemplate == null)
            {
                // Return 404: no existing carTemplate found
                return NotFound($"No existing CarTemplate with ID '{foundCarTemplate.ID}' found.");
            }

            var wasCarTemplateRemoved = _carTemplateControl.DeleteCarTemplate(ID);

            if (!wasCarTemplateRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete CarTemplate with ID '{foundCarTemplate.ID}' from database.");
            }

            // Return 204: No content (Successful deletion)
            return NoContent();
        }
    }
}
