using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.Models;

namespace ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerControl _accountControl;

        public CustomersController(ICustomerControl accountControl)
        {
            _accountControl = accountControl;
        }

        // GET https://localhost:7134/api/Accounts
        [HttpGet]
        public ActionResult<List<Customer>> GetAccounts()
        {
            var allAccounts = _accountControl.GetAllAccounts();

            if(allAccounts == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: List of accounts is null. " +
                                  "A bad GET request was made.");
            }

            if(allAccounts.Count == 0)
            {
                // Return 404: No accounts found
                return NotFound("ERROR: No accounts found in the database.");
            }

            // Return 200: OK
            return Ok(allAccounts);
        }

        // GET https://localhost:7134/api/Accounts/email
        [HttpGet("{email}")]
        public ActionResult<Customer> GetAccount(string email)
        {
            var foundAccount = _accountControl.GetAccountByEmail(email);
            
            if(foundAccount == null)
            {
                // Return 404: No account found, null
                return NotFound($"Customer with email '{email}' not found.");
            }

            // Return 200: OK
            return Ok(foundAccount);
        }

        // POST https://localhost:7134/api/Accounts
        [HttpPost]
        public ActionResult<Customer> CreateAccount([FromBody] Customer newAccount)
        {
            if(newAccount == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Customer request body.");
            }

            var wasAccountCreated = _accountControl.AddAccount(newAccount);

            if(wasAccountCreated)
            {
                // Return 201: Created Customer
                return CreatedAtAction(
                    nameof(GetAccount),
                    new { email = newAccount.Email},
                    newAccount);
            }

            // Return 409: Conflict if failure when creating account (e.g., duplicate email)
            return Conflict($"ERROR: Customer with email '{newAccount.Email}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT https://localhost:7134/api/Accounts/email
        [HttpPut("{email}")]
        public IActionResult UpdateAccount(string email, [FromBody] Customer updatedAccount)
        {
            if(updatedAccount == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Customer request body.");
            }

            var existingAccount = _accountControl.GetAccountByEmail(email);

            if(existingAccount == null)
            {
                // Return 404: no existing account found
                return NotFound($"No existing Customer with email '{existingAccount.Email}' found.");
            }

            if(existingAccount.Email != updatedAccount.Email)
            {
                // Return 409: Emails of existing account and response body account do not match.
                return Conflict($"Found Customer with email '{existingAccount.Email}' does not match email in request body '{updatedAccount.Email}'.");
            }

            var wasAccountUpdated = _accountControl.UpdateAccount(updatedAccount);

            if(!wasAccountUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update Customer with email '{email}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // DELETE: https://localhost:7134/api/Accounts/email
        [HttpDelete("{email}")]
        public IActionResult DeleteAccount(string email)
        {
            var foundAccount = _accountControl.GetAccountByEmail(email);

            if(foundAccount == null)
            {
                // Return 404: no existing account found
                return NotFound($"No existing Customer with email '{foundAccount.Email}' found.");
            }

            var wasAccountRemoved = _accountControl.DeleteAccount(email);

            if(!wasAccountRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete Customer with email '{foundAccount.Email}' from database.");
            }

            // Return 204: No content (Successful deletion)
            return NoContent();
        }
    }
}
