using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerControl _customerControl;

        public CustomersController(ICustomerControl customerControl)
        {
            _customerControl = customerControl;
        }

        // GET https://localhost:7134/api/Customers
        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            var allCustomers = _customerControl.GetAllCustomers();

            if(allCustomers == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: List of customers is null. " +
                                  "A bad GET request was made.");
            }

            if(allCustomers.Count == 0)
            {
                // Return 404: No accounts found
                return NotFound("ERROR: No customers found in the database.");
            }

            // Return 200: OK
            return Ok(allCustomers);
        }

        // GET https://localhost:7134/api/Customers/ID
        [HttpGet("{ID:int}")]
        public ActionResult<Customer> GetCustomer(int ID)
        {
            var foundCustomer = _customerControl.GetCustomerByID(ID);
            
            if(foundCustomer == null)
            {
                // Return 404: No customer found, null
                return NotFound($"Customer with ID '{ID}' not found.");
            }

            // Return 200: OK
            return Ok(foundCustomer);
        }

        // POST https://localhost:7134/api/Customers
        [HttpPost]
        public ActionResult<Customer> CreateCustomer([FromBody] Customer newCustomer)
        {
            if(newCustomer == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Customer request body.");
            }

            var wasCustomerCreated = _customerControl.AddCustomer(newCustomer);

            if(wasCustomerCreated)
            {
                // Return 201: Created Customer
                return CreatedAtAction(
                    nameof(GetCustomer),
                    new { ID = newCustomer.ID },
                    newCustomer);
            }

            // Return 409: Conflict if failure when creating account (e.g., duplicate email)
            return Conflict($"ERROR: Customer with ID '{newCustomer.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // DTO VERSION
        // POST https://localhost:7134/api/dto/Customers
        [HttpPost("dto")]
        public ActionResult<Customer> CreateCustomerDTO([FromBody] CustomerViewModel customerDTO)
        {
            if (customerDTO == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Customer request body.");
            }

            var wasCustomerCreated = _customerControl.AddCustomerDTO(customerDTO);

            if (wasCustomerCreated)
            {
                // Return 201: Created Customer
                return CreatedAtAction(
                    nameof(GetCustomer),
                    new { ID = customerDTO.ID },
                    customerDTO);
            }

            // Return 409: Conflict if failure when creating account (e.g., duplicate email)
            return Conflict($"ERROR: Customer with email '{customerDTO.Email}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT https://localhost:7134/api/dto/Customers/ID
        [HttpPut("{ID:int}")]
        public IActionResult UpdateCustomer(int ID, [FromBody] Customer updatedCustomer)
        {
            if(updatedCustomer == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Customer request body.");
            }

            var existingCustomer = _customerControl.GetCustomerByID(ID);

            if(existingCustomer == null)
            {
                // Return 404: no existing customer found
                return NotFound($"No existing Customer with ID '{existingCustomer.ID}' found.");
            }

            if(existingCustomer.ID != updatedCustomer.ID)
            {
                // Return 409: Emails of existing customer and response body customer do not match.
                return Conflict($"Found Customer with ID '{existingCustomer.ID}' does not match ID in request body '{updatedCustomer.ID}'.");
            }

            var wasCustomerUpdated = _customerControl.UpdateCustomer(updatedCustomer);

            if(!wasCustomerUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update Customer with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // DTO VERSION
        // PUT https://localhost:7134/api/dto/Customers/ID
        [HttpPut("dto/{ID:int}")]
        public IActionResult UpdateCustomerDTO(int ID, [FromBody] CustomerViewModel updatedCustomerDTO)
        {
            if (updatedCustomerDTO == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Customer request body.");
            }

            var existingCustomer = _customerControl.GetCustomerByID(ID);

            if (existingCustomer == null)
            {
                // Return 404: no existing customer found
                return NotFound($"No existing Customer with ID '{existingCustomer.ID}' found.");
            }

            if (existingCustomer.ID != updatedCustomerDTO.ID)
            {
                // Return 409: Emails of existing customer and response body customer do not match.
                return Conflict($"Found Customer with ID '{existingCustomer.ID}' does not match ID in request body '{updatedCustomerDTO.ID}'.");
            }

            var wasCustomerUpdated = _customerControl.UpdateCustomerDTO(updatedCustomerDTO);

            if (!wasCustomerUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update Customer with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }

        // DELETE: https://localhost:7134/api/Customers/ID
        [HttpDelete("{ID:int}")]
        public IActionResult DeleteCustomer(int ID)
        {
            var foundCustomer = _customerControl.GetCustomerByID(ID);

            if(foundCustomer == null)
            {
                // Return 404: no existing customer found
                return NotFound($"No existing Customer with ID '{foundCustomer.ID}' found.");
            }

            var wasCustomerRemoved = _customerControl.DeleteCustomer(ID);

            if(!wasCustomerRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete Customer with ID '{foundCustomer.ID}' from database.");
            }

            // Return 204: No content (Successful deletion)
            return NoContent();
        }
    }
}
