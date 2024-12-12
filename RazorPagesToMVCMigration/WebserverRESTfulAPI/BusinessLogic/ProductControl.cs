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

        public int GetProductID(string OEM)
        {
            int placeholderID = 0;
            try
            {
                placeholderID = _dbProductAccess.GetProductIDByOEM(OEM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return placeholderID;
        }

        public Product GetProductByID(int id)
        {
            Product productPlaceholder = null;
            try
            {
                productPlaceholder = _dbProductAccess.GetByIdentifier(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: no Product exists with ID '{id}'...: {ex.Message}");
            }
            return productPlaceholder;
        }

        public string GetProductName(Product product)
        {
            string namePlaceholder = "";
            try
            {
                namePlaceholder = _dbProductAccess.GetProductNameByID(product.ID);
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine($"Error: no existing product with the ID '{product.ID}' has a name '{namePlaceholder}': {ex.Message}");
            }
            return namePlaceholder;
        }
        public Product GetProduct(string OEM, string carPartName, string carVINNumber)
        {
            Product productPlaceholder = null;
            try
            {
                bool canProductBeRequested = _dbProductAccess.CarAndCarPartExists(carPartName, carVINNumber);
                if(!canProductBeRequested)
                {
                    throw new Exception("Either CarPart or Car does not exist.");
                }

                int productID = GetProductID(OEM);
                int carPartID = _dbProductAccess.GetCarPartIDByName(carPartName);
                int carID = _dbProductAccess.GetCarByVINNumber(carVINNumber);

                // Fetch product using inputs
                productPlaceholder = _dbProductAccess.GetByInputs(productID, carPartID, carID);
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine($"ArgumentException: The Product OEM number '{productPlaceholder.OEM}', its CarPart name '{carPartName}' or Car VIN number '{carVINNumber}' was incorrect or invalid.: {ex.Message}");
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
            int existingProductID = GetProductID(OEM);
            int numberOfRowsUpdated;
            try
            {
                bool carAndCarPartExist = _dbProductAccess.CarAndCarPartExists(carPartName, carVINNumber);
                if (!carAndCarPartExist)
                {
                    throw new ArgumentException($"We cannot find either a car with VIN {carVINNumber} and/or car part {carPartName} with the given inputs.");
                }

                int carPartID = _dbProductAccess.GetCarPartIDByName(carPartName);
                int carID = _dbProductAccess.GetCarByVINNumber(carVINNumber);
                productExists = _dbProductAccess.ProductExists(existingProductID);
                if (productExists) // CASE: Product does exist in DB.
                {
                    var myProduct = _dbProductAccess.GetByIdentifier(existingProductID);
                    if (myProduct.ItemAvailable == false)
                    {
                        throw new Exception("The Product is either sold out or no longer available!");
                    }
                    numberOfRowsUpdated = _dbProductAccess.UpdateEntity(product, existingProductID, carPartID, carID);
                    wasProductUpdated = (numberOfRowsUpdated == 1); // RETURNS TRUE ONLY IF NUMBER OF ROWS UPDATED IS EQUAL TO EXACTLY 1
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
            return wasProductUpdated;
        }

        public bool DeleteProduct(string OEM)
        {
            bool productExists;
            bool wasProductDeleted = false;
            try
            {
                int tempID = GetProductID(OEM);
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