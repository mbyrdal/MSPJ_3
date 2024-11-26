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

        // GET: https://localhost:7134/api/Products/ID
        [HttpGet("{ID:int}")]
        public ActionResult<Product> GetProduct(int ID)
        {
            var foundProduct = _productControl.GetProductByID(ID);

            if (foundProduct == null)
            {
                // Return 404: No product found, null
                return NotFound($"Product with ID '{ID}' not found.");
            }

            // Return 200: OK
            return Ok(foundProduct);
        }

        // POST: https://localhost:7134/api/Products/
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product newProduct)
        {
            if (newProduct == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Product request body.");
            }

            var wasProductCreated = _productControl.AddProduct(newProduct);

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

            // Return 409: Conflict by already existing ID (Product) or insertion fail
            // Multiple, identical products may have the OEM number inherited from CarModel. TODO: DETERMINE IS THIS TRUE ???
            return Conflict($"ERROR: Product with ID '{newProduct.ID}' already exists in the database, or insertion failed in another manner.");
        }

        // POST: https://localhost:7134/api/dto/Products/
        [HttpPost("dto")]
        public ActionResult<ProductViewModel> CreateProductDTO([FromBody] ProductViewModel newProduct)
        {
            if (newProduct == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Product request body.");
            }

            var wasProductCreated = _productControl.AddProductDTO(newProduct);

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
        // PUT: https://localhost:7134/api/Products/ID
        [HttpPut("{ID:int}")]
        public IActionResult UpdateProduct(int ID, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Product request body.");
            }

            var existingProduct = _productControl.GetProductByID(ID);

            if(existingProduct == null)
            {
                // Return 404: no existing product found
                return NotFound($"No existing Product with ID '{existingProduct.ID}' found.");
            }

            if(existingProduct.ID != updatedProduct.ID)
            {
                // Return 409: OEMs of existing product and response body product do not match.
                return Conflict($"Found Product with ID '{existingProduct.ID}' does not match ID in request body '{updatedProduct.ID}'.");
            }

            var wasProductUpdated = _productControl.UpdateProduct(updatedProduct);

            if(!wasProductUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update Product with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful update)
            return NoContent();
        }

        // TODO: trim and remove existingProduct logic since _productControl.UpdateProduct handles existing product issue already.
        // PUT: https://localhost:7134/api/Products/dto/ID
        [HttpPut("dto/{ID:int}")]
        public IActionResult UpdateProductDTO(int ID, [FromBody] ProductViewModel updatedProduct)
        {
            if (updatedProduct == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Product request body.");
            }

            var existingProduct = _productControl.GetProductByID(ID);

            if (existingProduct == null)
            {
                // Return 404: no existing product found
                return NotFound($"No existing Product with OEM '{existingProduct.OEM}' found.");
            }

            if (existingProduct.ID != updatedProduct.ID)
            {
                // Return 409: OEMs of existing product and response body product do not match.
                return Conflict($"Found Product with OEM '{existingProduct.OEM}' does not match OEM in request body '{updatedProduct.OEM}'.");
            }

            var wasProductUpdated = _productControl.UpdateProductDTO(updatedProduct);

            if (!wasProductUpdated)
            {
                // Return 500: Internal Server Error if the update fails
                return StatusCode(500, $"ERROR: Unable to update Product with ID '{ID}' in the database.");
            }

            // Return 204: No Content (Successful update)
            return NoContent();
        }

        // DELETE: https://localhost:7134/api/Products/ID
        [HttpDelete("{ID:int}")]
        public IActionResult DeleteProduct(int ID)
        {
            var foundProduct = _productControl.GetProductByID(ID);

            if(foundProduct == null)
            {
                // Return 404: no existing product found
                return NotFound($"No existing Product with ID '{foundProduct.ID}' found.");
            }

            var wasProductRemoved = _productControl.DeleteProduct(ID);

            if(!wasProductRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete Product with ID '{foundProduct.ID}' from database.");
            }

            // Return 204: No Content (Successful deletion)
            return NoContent();
        }
    }
}
