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

        public Product GetProduct(string OEM, string carPartName, string carVINNumber)
        {
            Product productPlaceholder = null;
            try
            {
                bool canProductBeRequsted = _dbProductAccess.CarAndCarPartExists(carPartName, carVINNumber);
                if(!canProductBeRequsted)
                {
                    throw new Exception("Either CarPart or Car does not exist.");
                }
                int productID = _dbProductAccess.GetProductIDByOEM(OEM);
                int carPartID = _dbProductAccess.GetCarPartIDByName(carPartName);
                int carID = _dbProductAccess.GetCarByVINNumber(carVINNumber);

                // Fetch product using inputs
                productPlaceholder = _dbProductAccess.GetByIdentifier(productID, carPartID, carID);
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine($"ArgumentException: The Product OEM number, its CarPart name or Car VIN number was incorrect or invalid.: {ex.Message}");
            }
            return productPlaceholder;
        }

        public Product GetProductByOEM(string OEM)
        {
            Product productPlaceholder = null;
            try
            {
                int productID = _dbProductAccess.GetProductIDByOEM(OEM);
                int carPartID = 
                productPlaceholder = _dbProductAccess.GetByIdentifier(productID);
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

        public bool AddProduct(ProductViewModel product, string carPartName, string carVINNumber)
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
                    numberOfRowsInserted = _dbProductAccess.CreateEntity(product, carPartID, carID);
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

        public bool UpdateProduct(string OEM, ProductViewModel product, string carPartName, string carVINNumber)
        {
            bool productExists = false;
            bool wasProductUpdated = false;
            int existingProductID = _dbProductAccess.GetProductIDByOEM(OEM);
            int numberOfRowsUpdated;
            try
            {
                bool carAndCarPartExist = _dbProductAccess.CarAndCarPartExists(carPartName, carVINNumber);
                if (!carAndCarPartExist)
                {
                    throw new ArgumentException($"We cannot find either a car with VIN {carVINNumber}, and/or car part {carPartName} with the given inputs.");
                }

                int carPartID = _dbProductAccess.GetCarPartIDByName(carPartName);
                int carID = _dbProductAccess.GetCarByVINNumber(carVINNumber);
                productExists = _dbProductAccess.ProductExists(existingProductID);
                if (productExists) // CASE: Product does exist in DB.
                {
                    numberOfRowsUpdated = _dbProductAccess.UpdateEntity(product, existingProductID, carPartID, carID);
                    wasProductUpdated = (numberOfRowsUpdated == 1); // RETURNS TRUE ONLY IF NUMBER OF ROWS UPDATED IS EQUAL TO EXACTLY 1
                }
            }
            catch (Exception ex)
            {
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
                int tempID = _dbProductAccess.GetProductIDByOEM(OEM);
                productExists = _dbProductAccess.ProductExists(tempID);
                if(productExists) // CASE: Product does exist in DB.
                {
                    wasProductDeleted = _dbProductAccess.DeleteEntity(tempID);
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