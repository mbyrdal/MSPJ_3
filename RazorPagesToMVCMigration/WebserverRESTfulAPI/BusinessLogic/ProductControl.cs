using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Models;
using System.Diagnostics;

namespace ServiceAPI.BusinessLogic
{
    public class ProductControl : IProductControl
    {
        private readonly DbProduct _dbProductAccess;
        public ProductControl(IConfiguration configuration)
        {
            _dbProductAccess = new DbProduct(configuration);
        }
        public Product GetProductByOEM(string OEM)
        {
            Product productPlaceholder = null;
            try
            {
                productPlaceholder = _dbProductAccess.GetByIdentifier(OEM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return productPlaceholder;
        }
        public List<Product> GetAllProducts()
        {
            List<Product> allProducts = new List<Product>();
            try
            {
                allProducts = _dbProductAccess.GetAllEntities();
            }
            catch (Exception ex)
            {
                allProducts = null;
                Debug.WriteLine(ex.Message);
            }
            return allProducts;
        }
        public bool AddProduct(Product product)
        {
            bool productExists = false;
            bool wasProductInserted = false;
            int numberOfRowsInserted;
            try
            {
                productExists = _dbProductAccess.ProductExists(product.OEM); // Product exists based on whether its OEM number can be found in the Product table.
                if(!productExists) // CASE: Product does not exist in DB.
                {
                    numberOfRowsInserted = _dbProductAccess.CreateEntity(product);
                    wasProductInserted = (numberOfRowsInserted == 1); // If only one row was inserted, then we can determine that the product was added correctly.
                }
            }
            catch(Exception ex)
            {
                product = null;
                Debug.WriteLine(ex.Message);
            }
            return wasProductInserted;
        }
        public bool UpdateProduct(Product product)
        {
            bool productExists = false;
            bool wasProductUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                productExists = _dbProductAccess.ProductExists(product.OEM);
                if(productExists) // CASE: Product does exist in DB.
                {
                    numberOfRowsUpdated = _dbProductAccess.UpdateEntity(product);
                    wasProductUpdated = (numberOfRowsUpdated == 1);
                }
            }
            catch( Exception ex)
            {
                product = null;
                Debug.WriteLine(ex.Message);
            }
            return wasProductUpdated;
        }
        public bool DeleteProduct(string OEM)
        {
            bool productExists;
            bool wasProductDeleted = false;
            try
            {
                productExists = _dbProductAccess.ProductExists(OEM);
                if(productExists) // CASE: Product does exist in DB.
                {
                    wasProductDeleted = _dbProductAccess.DeleteEntity(OEM);
                }
            }
            catch (Exception ex)
            {
                wasProductDeleted = false;
                Debug.WriteLine(ex.Message);
            }
            return wasProductDeleted;
        }
    }
}
