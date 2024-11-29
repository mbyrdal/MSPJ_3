using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.DTOs;
using ServiceAPI.Models;
using System.Diagnostics;

namespace ServiceAPI.BusinessLogic
{
    public class ProductControl : IProductControl
    {
        private readonly DbProduct _dbProductAccess;

        public ProductControl(DbProduct dbProductAccess)
        {
            _dbProductAccess = dbProductAccess;
        }

        public Product GetProductByID(int ID)
        {
            Product productPlaceholder = null;
            try
            {
                productPlaceholder = _dbProductAccess.GetByIdentifier(ID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return productPlaceholder;
        }

        public Product GetProductByOEM(string OEM)
        {
            Product productPlaceholder = null;
            try
            {
                var productIDPlaceholder = _dbProductAccess.GetProductIDByOEM(OEM);
                productPlaceholder = _dbProductAccess.GetByIdentifier(productIDPlaceholder);
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
                productExists = _dbProductAccess.ProductExists(product.ID); // Product exists based on whether its ID number can be found in the Product table.
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

        public bool AddProductDTO(ProductViewModel product)
        {
            bool productExists = false;
            bool wasProductInserted = false;
            int numberOfRowsInserted;
            try
            {
                productExists = _dbProductAccess.ProductExists(product.ID); // Product exists based on whether its ID number can be found in the Product table.
                if (!productExists) // CASE: Product does not exist in DB.
                {
                    numberOfRowsInserted = _dbProductAccess.CreateEntityDTO(product);
                    wasProductInserted = (numberOfRowsInserted == 1); // If only one row was inserted, then we can determine that the product was added correctly.
                }
            }
            catch (Exception ex)
            {
                product = null;
                Debug.WriteLine(ex.Message);
            }
            return wasProductInserted;
        }

        public bool AddProductDTO(ProductViewModel product, string carPartName, string carVINNumber)
        {
            bool productExists = false;
            bool carAndCarPartExists = false;
            int carPartID;
            int carID;

            bool wasProductInserted = false;
            int numberOfRowsInserted;


            try
            {
                carAndCarPartExists = _dbProductAccess.CarAndCarPartExists(carPartName, carVINNumber);

                if (!carAndCarPartExists) // CASE: Car or car part does not exist in DB.
                {
                    throw new Exception("Either car or car part does not exist");
                }

                carPartID = _dbProductAccess.GetCarPartIDByName(carPartName);
                carID = _dbProductAccess.GetCarByVINNumber(carVINNumber);

                productExists = _dbProductAccess.ProductExists(product.ID); // Product exists based on whether its ID number can be found in the Product table.
                if (!productExists) // CASE: Product does not exist in DB.
                {
                    numberOfRowsInserted = _dbProductAccess.CreateEntityDTO(product, carPartID, carID);
                    wasProductInserted = (numberOfRowsInserted == 1); // If only one row was inserted, then we can determine that the product was added correctly.
                }
            }
            catch (Exception ex)
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
                var existingProduct = _dbProductAccess.GetByIdentifier(product.ID);
                product.ID = existingProduct.ID;
                productExists = _dbProductAccess.ProductExists(product.ID);
                if(productExists) // CASE: Product does exist in DB.
                {
                    numberOfRowsUpdated = _dbProductAccess.UpdateEntity(product);
                    wasProductUpdated = (numberOfRowsUpdated == 1);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasProductUpdated;
        }

        public bool UpdateProduct(string OEM, Product product)
        {
            bool productExists = false;
            bool wasProductUpdated = false;
            int existingProductID = _dbProductAccess.GetProductIDByOEM(OEM);
            int numberOfRowsUpdated;
            try
            {
                productExists = _dbProductAccess.ProductExists(existingProductID);
                if (productExists) // CASE: Product does exist in DB.
                {
                    numberOfRowsUpdated = _dbProductAccess.UpdateEntity(product, existingProductID);
                    wasProductUpdated = (numberOfRowsUpdated == 1);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasProductUpdated;
        }

        public bool UpdateProductDTO(ProductViewModel product)
        {
            bool productExists = false;
            bool wasProductUpdated = false;
            int numberOfRowsUpdated;
            try
            {
                productExists = _dbProductAccess.ProductExists(product.ID);
                if (productExists) // CASE: Product does exist in DB.
                {
                    numberOfRowsUpdated = _dbProductAccess.UpdateEntityDTO(product);
                    wasProductUpdated = (numberOfRowsUpdated == 1);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return wasProductUpdated;
        }

        public bool DeleteProduct(int ID)
        {
            bool productExists;
            bool wasProductDeleted = false;
            try
            {
                productExists = _dbProductAccess.ProductExists(ID);
                if(productExists) // CASE: Product does exist in DB.
                {
                    wasProductDeleted = _dbProductAccess.DeleteEntity(ID);
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