using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.Models;

namespace ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly IGuestControl _guestControl;

        public GuestsController(IGuestControl guestControl)
        {
            _guestControl = guestControl;
        }

        // GET: https://localhost:7134/api/Guests
        [HttpGet]
        public ActionResult<List<Guest>> GetGuests()
        {
            var allGuests = _guestControl.GetAllGuests();

            if(allGuests == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: List of guests is null. A bad GET request was made.");
            }

            if(allGuests.Count == 0)
            {
                // Return 404: No guests found
                return NotFound("ERROR: No guests found in the database.");
            }

            // Return 200: OK
            return Ok(allGuests);
        }

        // GET: https://localhost:7134/api/Guests/email
        [HttpGet("{email}")]
        public ActionResult<Guest> GetGuest(string email)
        {
            var foundGuest = _guestControl.GetGuestByEmail(email);

            if(foundGuest == null)
            {
                // Return 404: No guest found, null
                return NotFound($"Guest with email '{email}' not found.");
            }

            // Return 200: OK
            return Ok(foundGuest);
        }

        // POST: https://localhost:7134/api/Guests
        [HttpPost]
        public ActionResult<Guest> CreateGuest([FromBody] Guest newGuest)
        {
            if(newGuest == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Account request body.");
            }

            var wasGuestCreated = _guestControl.AddGuest(newGuest);

            if(wasGuestCreated)
            {
                // Return 201: Created Guest
                return CreatedAtAction(nameof(GetGuest), new { email = newGuest.Email }, newGuest);
            }

            // Return 409: Conflict if failure when creating guest (e.g., duplicate email)
            return Conflict($"ERROR: Guest with email '{newGuest.Email}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT: https://localhost:7134/api/Guests/email
        [HttpPut("{email}")]
        public IActionResult UpdateGuest(string email, [FromBody] Guest updatedGuest)
        {
            if(updatedGuest == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Account request body.");
            }

            var existingGuest = _guestControl.GetGuestByEmail(email);

            if(existingGuest == null)
            {
                // Return 404: no existing guest found
                return NotFound($"No existing Guest with email '{existingGuest.Email}' found.");
            }

            if(existingGuest.Email != updatedGuest.Email)
            {
                // Return 409: Emails of existing guest and response body guest do not match.
                return Conflict($"Found Guest with email '{existingGuest.Email}' does not match email in request body '{updatedGuest.Email}'.");
            }

            var wasGuestUpdated = _guestControl.UpdateGuest(updatedGuest);

            if(!wasGuestUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update Guest with email '{email}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // DELETE: https://localhost:7134/api/Guests/email
        [HttpDelete("{email}")]
        public IActionResult DeleteGuest(string email)
        {
            var foundGuest = _guestControl.GetGuestByEmail(email);
            
            if(foundGuest == null)
            {
                // Return 404: no existing guest found
                return NotFound($"No existing Guest with email '{foundGuest.Email}' found.");
            }

            var wasGuestRemoved = _guestControl.DeleteGuest(email);

            if(!wasGuestRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete Guest with email '{foundGuest.Email}' from database.");
            }

            // Return 204: No content (Successful deletion)
            return NoContent();
        }

    }
}
