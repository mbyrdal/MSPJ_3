using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.BusinessLogic.Services;
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
        // GET: ProductsController/Products
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            var allProducts = _productControl.GetAllProducts();
            if (allProducts == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: List of products is null. A bad GET request was made.");
                
            }
            else if(allProducts.Count == 0)
            {
                // Return 404: No products found
                return NotFound("ERROR: No products found in the database.");
            }
            else
            {
                // Return 200: OK
                return Ok(allProducts);
            }
        }
        // GET: ProductsController/Products/{OEM}
        [HttpGet("{OEM}")]
        public ActionResult<Product> GetProduct(string OEM)
        {
            var foundProduct = _productControl.GetProductByOEM(OEM);
            if (foundProduct == null)
            {
                // Return 404: no product found, null
                return NotFound($"Product with OEM '{OEM}' not found.");
            }
            // Return 200: OK
            return Ok(foundProduct);
        }
        // POST: ProductsController/CreateProduct
        [HttpPost]
        public ActionResult CreateProduct([FromBody] Product newProduct)
        {
            if (newProduct == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Product request body.");
            }
            var wasProductCreated = _productControl.AddProduct(newProduct);
            if(wasProductCreated)
            {
                // Return 201: Successful creation (add) of new Product in DB
                // Procedure below:
                // CreatedAtAction response object is 201
                // nameof(...) determines action method to be used
                // new {...} determines input parameters
                // newProduct is response object
                return CreatedAtAction(nameof(GetProduct), new {OEM =  newProduct.OEM}, newProduct);
            }
            else
            {
                //  Return 409: Conflict by already existing OEM (Product) or insertion fail
                return Conflict($"ERROR: Product with OEM: '{newProduct.OEM}' already exists in the Database, or insertion failed in another manner.");
            }
        }
        [HttpPut("{OEM}")]
        public ActionResult UpdateProduct([FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                // Return 400: Bad request response
                return BadRequest("ERROR: Bad Product request body.");
            }
            var wasProductUpdated = _productControl.UpdateProduct(updatedProduct);
            if (!wasProductUpdated)
            {
                var foundProduct = _productControl.GetProductByOEM(updatedProduct.OEM);
                if(foundProduct == null)
                {
                    // Return 404: no existing product found
                    return NotFound($"No existing Product with OEM: '{foundProduct.OEM}' found.");
                }
                else if(foundProduct.OEM != updatedProduct.OEM)
                {
                    // Return 409: OEMs of existing product and response body product do not match.
                    return Conflict($"Found Product with OEM: '{foundProduct.OEM}' does not match OEM in request body: '{updatedProduct.OEM}'.");
                }
            }
            // Return 200: OK
            return Ok("Product updated successfully.");
        }
        [HttpDelete("{OEM}")]
        public ActionResult DeleteProduct(string OEM)
        {
            var foundProduct = _productControl.GetProductByOEM(OEM);
            if(foundProduct == null)
            {
                // Return 404: no existing product found
                return NotFound($"No existing Product with OEM: '{foundProduct.OEM}' found.");
            }
            var wasProductRemoved = _productControl.DeleteProduct(OEM);
            if(!wasProductRemoved)
            {
                return StatusCode(500, $"ERROR: Unable to delete Product with OEM: '{foundProduct.OEM}' from database.");
            }
            return Ok("Product removed successfully.");
        }
    }
}
