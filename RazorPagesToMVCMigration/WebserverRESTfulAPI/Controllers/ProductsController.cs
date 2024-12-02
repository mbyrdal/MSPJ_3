using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductControl _productControl;

        public ProductsController(IProductControl productControl)
        {
            _productControl = productControl;
        }

        // GET: https://localhost:7134/api/Products
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var allProducts = await _productControl.GetAllProductsAsync();

            if (allProducts == null)
            {
                return BadRequest("ERROR: List of products is null. A bad GET request was made.");
            }

            if (allProducts.Count == 0)
            {
                return NotFound("ERROR: No products found in the database.");
            }

            return Ok(allProducts);
        }

        // GET: https://localhost:7134/api/Products/OEM
        [HttpGet("{OEM}")]
        public async Task<ActionResult<Product>> GetProduct(string OEM)
        {
            var foundProduct = await _productControl.GetProductByOEMAsync(OEM);

            if (foundProduct == null)
            {
                return NotFound($"Product with OEM '{OEM}' not found.");
            }

            return Ok(foundProduct);
        }

        // POST: https://localhost:7134/api/Products
        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> CreateProduct([FromBody] ProductViewModel newProduct, string name, string vinNumber)
        {
            if (newProduct == null)
            {
                return BadRequest("ERROR: Bad Product request body.");
            }

            var wasProductCreated = await _productControl.AddProductAsync(newProduct, name, vinNumber);

            if (wasProductCreated)
            {
                return CreatedAtAction(
                    nameof(GetProduct),
                    new { OEM = newProduct.OEM },
                    newProduct);
            }

            return Conflict($"ERROR: Product with OEM '{newProduct.OEM}' already exists in the database, or insertion failed in another manner.");
        }

        // PUT: https://localhost:7134/api/Products/OEM
        [HttpPut("{OEM}")]
        public async Task<IActionResult> UpdateProduct(string OEM, [FromBody] ProductViewModel updatedProduct, string carPartName, string carVINNumber)
        {
            if (updatedProduct == null)
            {
                return BadRequest("ERROR: Bad Product request body.");
            }

            var existingProduct = await _productControl.GetProductByOEMAsync(OEM);

            if (existingProduct == null)
            {
                return NotFound($"No existing Product with OEM '{OEM}' found.");
            }

            if (OEM != existingProduct.OEM)
            {
                return Conflict($"Found Product with OEM '{OEM}' does not match OEM in request body '{existingProduct.OEM}'.");
            }

            var wasProductUpdated = await _productControl.UpdateProductAsync(OEM, updatedProduct, carPartName, carVINNumber);

            if (!wasProductUpdated)
            {
                return StatusCode(500, $"ERROR: Unable to update Product with OEM '{OEM}' in the database.");
            }

            return NoContent();
        }

        // DELETE: https://localhost:7134/api/Products/OEM
        [HttpDelete("{OEM}")]
        public async Task<IActionResult> DeleteProduct(string OEM)
        {
            var foundProduct = await _productControl.GetProductByOEMAsync(OEM);

            if (foundProduct == null)
            {
                return NotFound($"No existing Product with OEM '{OEM}' found.");
            }

            var wasProductRemoved = await _productControl.DeleteProductAsync(OEM);

            if (!wasProductRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete Product with OEM '{OEM}' from database.");
            }

            return NoContent();
        }
    }
}
