using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<List<CarTemplate>>> GetCarTemplates()
        {
            var allCarTemplates = await _carTemplateControl.GetAllCarTemplatesAsync();

            if (allCarTemplates == null)
            {
                return BadRequest("ERROR: List of carTemplates is null. A bad GET request was made.");
            }

            if (allCarTemplates.Count == 0)
            {
                return NotFound("ERROR: No carTemplates found in the database.");
            }

            return Ok(allCarTemplates);
        }

        // GET https://localhost:7134/api/cartemplates/ID
        [HttpGet("{ID:int}")]
        public async Task<ActionResult<CarTemplate>> GetCarTemplate(int ID)
        {
            var foundCarTemplate = await _carTemplateControl.GetCarTemplateByIDAsync(ID);

            if (foundCarTemplate == null)
            {
                return NotFound($"CarTemplate with ID '{ID}' not found.");
            }

            return Ok(foundCarTemplate);
        }

        // POST https://localhost:7134/api/cartemplates
        [HttpPost]
        public async Task<ActionResult<CarTemplate>> CreateCarTemplate([FromBody] CarTemplate newCarTemplate)
        {
            if (newCarTemplate == null)
            {
                return BadRequest("ERROR: Bad CarTemplate request body.");
            }

            var wasCarTemplateCreated = await _carTemplateControl.AddCarTemplateAsync(newCarTemplate);

            if (wasCarTemplateCreated)
            {
                return CreatedAtAction(
                    nameof(GetCarTemplate),
                    new { ID = newCarTemplate.ID },
                    newCarTemplate);
            }

            return Conflict($"ERROR: CarTemplate with ID '{newCarTemplate.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT https://localhost:7134/api/cartemplates/ID
        [HttpPut("{ID:int}")]
        public async Task<IActionResult> UpdateCarTemplate(int ID, [FromBody] CarTemplate updatedCarTemplate)
        {
            if (updatedCarTemplate == null)
            {
                return BadRequest("ERROR: Bad CarTemplate request body.");
            }

            var existingCarTemplate = await _carTemplateControl.GetCarTemplateByIDAsync(ID);

            if (existingCarTemplate == null)
            {
                return NotFound($"No existing CarTemplate with ID '{ID}' found.");
            }

            if (existingCarTemplate.ID != updatedCarTemplate.ID)
            {
                return Conflict($"Found CarTemplate with ID '{existingCarTemplate.ID}' does not match ID in request body '{updatedCarTemplate.ID}'.");
            }

            var wasCarTemplateUpdated = await _carTemplateControl.UpdateCarTemplateAsync(updatedCarTemplate);

            if (!wasCarTemplateUpdated)
            {
                return StatusCode(500, $"ERROR: Unable to update CarTemplate with ID '{ID}' in the database.");
            }

            return NoContent();
        }

        // DELETE: https://localhost:7134/api/cartemplates/ID
        [HttpDelete("{ID:int}")]
        public async Task<IActionResult> DeleteCarTemplate(int ID)
        {
            var foundCarTemplate = await _carTemplateControl.GetCarTemplateByIDAsync(ID);

            if (foundCarTemplate == null)
            {
                return NotFound($"No existing CarTemplate with ID '{ID}' found.");
            }

            var wasCarTemplateRemoved = await _carTemplateControl.DeleteCarTemplateAsync(ID);

            if (!wasCarTemplateRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete CarTemplate with ID '{ID}' from database.");
            }

            return NoContent();
        }
    }
}
