using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic.Services;
using ServiceAPI.Models;

namespace ServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductsController/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var allProducts = _productService.GetAll();

            if (allProducts == null)
            {
                return StatusCode(500, "Failed to fetch the product list.");
            }

            if (!allProducts.Any())
            {
                return NotFound("No products available.");
            }

            return Ok(allProducts);
        }

        // GET: ProductsController/Products/{OEM}
        [HttpGet("{OEM}")]
        public ActionResult<Product> GetProduct(string OEM)
        {
            // Fetch product by OEM
            var foundProduct = _productService.GetById(OEM);

            if (foundProduct == null)
            {
                // Return 404: Not Found
                return NotFound($"Product with OEM '{OEM}' not found.");
            }

            // Return 200: OK with product details
            return Ok(foundProduct);
        }

        // POST: ProductsController/CreateProduct
        [HttpPost]
        public ActionResult CreateProduct([FromBody] Product newProduct)
        {
            try
            {
                if (newProduct == null)
                {
                    return BadRequest();
                }

                // Adds product to DB
                var createdProd = _productService.Create(newProduct);

                // Deprecated version: use return Created() ...
                // CreatedAtAction: controller runner with the ability to return status codes
                // Return 201 status code along with the location of the newly created Product ...
                return CreatedAtAction(nameof(GetProduct), // Action (GetProduct) to fetch Product
                                       new { OEM = createdProd }, // Route parameter
                                       createdProd); // Created Product part of response body

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{OEM}")]
        public ActionResult UpdateProduct(string OEM, [FromBody] Product newProduct)
        {
            if (OEM != newProduct.OEM)
            {
                return BadRequest("OEM mismatch between URL and supplied product for update.");
            }

            var existingProduct = _productService.GetById(OEM);

            if (existingProduct == null)
            {
                return NotFound($"Product with OEM '{OEM}' not found.");
            }

            // Update product in the database
            var updateResult = _productService.Update(newProduct);

            if (!updateResult)
            {
                return StatusCode(500, "Failed to update the product.");
            }

            return Ok("Product updated successfully.");
        }

        [HttpDelete("{OEM}")]
        public ActionResult DeleteProduct(string OEM)
        {
            var foundProduct = _productService.GetById(OEM);
            if(foundProduct == null)
            {
                return NotFound($"Product with OEM {OEM} does not exist in the database.");
            }
            var removedProduct = _productService.Delete(OEM);
            return Ok(removedProduct);
        }
    }
}
