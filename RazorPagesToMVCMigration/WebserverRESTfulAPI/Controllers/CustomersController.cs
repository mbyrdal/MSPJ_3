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
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            var allCustomers = await _customerControl.GetAllCustomersAsync();

            if (allCustomers == null)
            {
                return BadRequest("ERROR: List of customers is null. A bad GET request was made.");
            }

            if (allCustomers.Count == 0)
            {
                return NotFound("ERROR: No customers found in the database.");
            }

            return Ok(allCustomers);
        }

        // GET https://localhost:7134/api/Customers/ID
        [HttpGet("{ID:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int ID)
        {
            var foundCustomer = await _customerControl.GetCustomerByIDAsync(ID);

            if (foundCustomer == null)
            {
                return NotFound($"Customer with ID '{ID}' not found.");
            }

            return Ok(foundCustomer);
        }

        // POST https://localhost:7134/api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer newCustomer)
        {
            if (newCustomer == null)
            {
                return BadRequest("ERROR: Bad Customer request body.");
            }

            var wasCustomerCreated = await _customerControl.AddCustomerAsync(newCustomer);

            if (wasCustomerCreated)
            {
                return CreatedAtAction(
                    nameof(GetCustomer),
                    new { ID = newCustomer.ID },
                    newCustomer);
            }

            return Conflict($"ERROR: Customer with ID '{newCustomer.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // DTO VERSION
        // POST https://localhost:7134/api/dto/Customers
        [HttpPost("dto")]
        public async Task<ActionResult<Customer>> CreateCustomerDTO([FromBody] CustomerViewModel customerDTO)
        {
            if (customerDTO == null)
            {
                return BadRequest("ERROR: Bad Customer request body.");
            }

            var wasCustomerCreated = await _customerControl.AddCustomerDTOAsync(customerDTO);

            if (wasCustomerCreated)
            {
                return CreatedAtAction(
                    nameof(GetCustomer),
                    new { ID = customerDTO.ID },
                    customerDTO);
            }

            return Conflict($"ERROR: Customer with email '{customerDTO.Email}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT https://localhost:7134/api/Customers/ID
        [HttpPut("{ID:int}")]
        public async Task<IActionResult> UpdateCustomer(int ID, [FromBody] Customer updatedCustomer)
        {
            if (updatedCustomer == null)
            {
                return BadRequest("ERROR: Bad Customer request body.");
            }

            var existingCustomer = await _customerControl.GetCustomerByIDAsync(ID);

            if (existingCustomer == null)
            {
                return NotFound($"No existing Customer with ID '{ID}' found.");
            }

            if (existingCustomer.ID != updatedCustomer.ID)
            {
                return Conflict($"Found Customer with ID '{existingCustomer.ID}' does not match ID in request body '{updatedCustomer.ID}'.");
            }

            var wasCustomerUpdated = await _customerControl.UpdateCustomerAsync(updatedCustomer);

            if (!wasCustomerUpdated)
            {
                return StatusCode(500, $"ERROR: Unable to update Customer with ID '{ID}' in the database.");
            }

            return NoContent();
        }

        // DTO VERSION
        // PUT https://localhost:7134/api/dto/Customers/ID
        [HttpPut("dto/{ID:int}")]
        public async Task<IActionResult> UpdateCustomerDTO(int ID, [FromBody] CustomerViewModel updatedCustomerDTO)
        {
            if (updatedCustomerDTO == null)
            {
                return BadRequest("ERROR: Bad Customer request body.");
            }

            var existingCustomer = await _customerControl.GetCustomerByIDAsync(ID);

            if (existingCustomer == null)
            {
                return NotFound($"No existing Customer with ID '{ID}' found.");
            }

            if (existingCustomer.ID != updatedCustomerDTO.ID)
            {
                return Conflict($"Found Customer with ID '{existingCustomer.ID}' does not match ID in request body '{updatedCustomerDTO.ID}'.");
            }

            var wasCustomerUpdated = await _customerControl.UpdateCustomerDTOAsync(updatedCustomerDTO);

            if (!wasCustomerUpdated)
            {
                return StatusCode(500, $"ERROR: Unable to update Customer with ID '{ID}' in the database.");
            }

            return NoContent();
        }

        // DELETE: https://localhost:7134/api/Customers/ID
        [HttpDelete("{ID:int}")]
        public async Task<IActionResult> DeleteCustomer(int ID)
        {
            var foundCustomer = await _customerControl.GetCustomerByIDAsync(ID);

            if (foundCustomer == null)
            {
                return NotFound($"No existing Customer with ID '{ID}' found.");
            }

            var wasCustomerRemoved = await _customerControl.DeleteCustomerAsync(ID);

            if (!wasCustomerRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete Customer with ID '{ID}' from the database.");
            }

            return NoContent();
        }
    }
}
