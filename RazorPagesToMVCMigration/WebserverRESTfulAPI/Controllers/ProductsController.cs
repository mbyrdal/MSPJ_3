using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic;
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
        public ActionResult<List<Product>> GetProducts()
        {
            var allProducts = _productControl.GetAllProducts();

            if (allProducts == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: List of products is null. A bad GET request was made.");
                
            }

            if(allProducts.Count == 0)
            {
                // Return 404: No products found
                return NotFound("ERROR: No products found in the database.");
            }

            // Return 200: OK
            return Ok(allProducts);
        }

        // GET: https://localhost:7134/api/Products/OEM/
        [HttpGet("{OEM}")]
        public ActionResult<Product> GetProductUsingOEM(string OEM)
        {
            int productID = _productControl.GetProductID(OEM);
            var foundProduct = _productControl.GetProductByID(productID);

            if (foundProduct == null)
            {
                // Return 404: No product found, null
                return NotFound($"Product with OEM '{OEM}' not found.");
            }

            // Return 200: OK
            return Ok(foundProduct);
        }

        // GET: https://localhost:7134/api/Products/OEM/carPartName/carVINNumber
        [HttpGet("{OEM}/{carPartName}/{carVINNumber}")]
        public ActionResult<Product> GetProduct(string OEM, string carPartName, string carVINNumber)
        {
            var foundProduct = _productControl.GetProduct(OEM, carPartName, carVINNumber);

            if (foundProduct == null)
            {
                // Return 404: No product found, null
                return NotFound($"Product with OEM '{OEM}', Name '{carPartName}' and VIN number '{carVINNumber}' not found.");
            }

            // Return 200: OK
            return Ok(foundProduct);
        }

        // POST: https://localhost:7134/api/Products
        [HttpPost]
        public ActionResult<ProductViewModel> CreateProduct(string carPartName, string carVINNumber, [FromBody] ProductViewModel newProduct)
        {
            if (newProduct == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Product request body.");
            }

            var wasProductCreated = _productControl.AddProduct(newProduct, carPartName, carVINNumber);

            if (wasProductCreated)
            {
                // Return 201: Successful creation (add) of new Product in DB
                // Procedure below:
                // CreatedAtAction response object is 201
                // nameof(...) determines action method to be used
                // new {...} determines input parameters
                // newProduct is response object
                return CreatedAtAction(nameof(GetProduct), new { ID = newProduct.ID }, newProduct);
            }

            // Return 409: Conflict by already existing OEM (ProductViewModel) or insertion fail
            // Multiple, identical products may have the OEM number inherited from CarModel. TODO: DETERMINE IS THIS TRUE ???
            return Conflict($"ERROR: Product with OEM '{newProduct.OEM}' already exists in the database, or insertion failed in another manner.");
        }

        // TODO: trim and remove existingProduct logic since _productControl.UpdateProduct handles existing product issue already.
        // PUT: https://localhost:7134/api/Products/OEM/carPartName/carVINNumber
        [HttpPut("{OEM}/{carPartName}/{carVINNumber}")]
        public IActionResult UpdateProduct(string OEM, string carPartName, string carVINNumber, [FromBody] ProductViewModel updatedProduct)
        {
            if (updatedProduct == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Product request body.");
            }

            var existingProduct = _productControl.GetProduct(OEM, carPartName, carVINNumber);

            if(existingProduct == null)
            {
                // Return 404: no existing product found
                return NotFound($"No existing Product with OEM '{OEM}' found.");
            }

            if(OEM != existingProduct.OEM)
            {
                // Return 409: OEMs of existing product and response body product do not match.
                return Conflict($"Found Product with OEM '{OEM}' does not match OEM in request body '{existingProduct.OEM}'.");
            }

            var wasProductUpdated = _productControl.UpdateProduct(OEM, updatedProduct, carPartName, carVINNumber);

            if(!wasProductUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update Product with OEM '{OEM}' in the database.");
            }

            // Return 204: No Content (Successful update)
            return NoContent();
        }

        // DELETE: https://localhost:7134/api/Products/OEM
        [HttpDelete("{OEM}")]
        public IActionResult DeleteProduct(string OEM)
        {
            int productID = _productControl.GetProductID(OEM);
            var foundProduct = _productControl.GetProductByID(productID);

            if(foundProduct == null)
            {
                // Return 404: no existing product found
                return NotFound($"No existing Product with OEM '{foundProduct}' found.");
            }

            var wasProductRemoved = _productControl.DeleteProduct(OEM);

            if(!wasProductRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete Product with OEM '{foundProduct.OEM}' from database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }
    }
}
