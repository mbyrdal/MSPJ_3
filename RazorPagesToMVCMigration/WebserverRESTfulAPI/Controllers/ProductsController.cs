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
                return BadRequest("List of Products cannot be null.");
            }
            else if (allProducts.Count() == 0)
            {
                return NotFound("List of Products is empty.");
            }
            return Ok(allProducts);
        }

        // GET: ProductsController/Products/{OEM}
        [HttpGet("{OEM:string}")]
        public ActionResult<Product> GetProduct(string OEM)
        {
            var foundProduct = _productService.GetById(OEM);
            try
            {
                if (foundProduct == null)
                {
                    // Status code 404, not found response
                    return NotFound($"Product with OEM {OEM} not found (NULL).");
                }
                // Status code 200, OK
                return Ok(foundProduct);
            }

            // Status code 400, bad request response
            catch (ArgumentException)
            {
                return BadRequest();
            }
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
            if(OEM != newProduct.OEM)
            {
                return BadRequest("OEM mismatch between and supplied product for update.");
            }
            var existingProduct = _productService.GetById(OEM);
            if(existingProduct == null)
            {
                return NotFound("Product found but missing details...");
            }
            existingProduct.OEM = newProduct.OEM;
            existingProduct.VINNumber = newProduct.VINNumber;
            existingProduct.Name = newProduct.Name;
            existingProduct.Price = newProduct.Price;
            existingProduct.DateAvailable = newProduct.DateAvailable;
            existingProduct.Notes = newProduct.Notes;

            return Ok(existingProduct);
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
