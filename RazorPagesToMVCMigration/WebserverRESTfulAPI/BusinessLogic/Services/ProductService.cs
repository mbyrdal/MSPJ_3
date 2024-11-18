using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess.DatabaseEntities;
using ServiceAPI.Models;
using System.Diagnostics;

namespace ServiceAPI.BusinessLogic.Services
{
    public class ProductService : ICRUD<Product>
    {
        private readonly DbEntity_Product _DbProductAccess;

        public ProductService(IConfiguration configuration)
        {
            _DbProductAccess = new DbEntity_Product(configuration);
        }

        public bool Create(Product entity)
        {
            bool wasProductInserted;
            int numberOfRowsInserted;

            try
            {
                numberOfRowsInserted = _DbProductAccess.Create(entity);
                wasProductInserted = (numberOfRowsInserted == 1);
            }
            // TODO: implement proper exception when Product was not inserted...
            catch (Exception ex) 
            {
                wasProductInserted = false;
                Debug.WriteLine(ex.Message);
            }
            return wasProductInserted;
        }

        public bool Delete(string OEM)
        {
            bool wasProductDeleted;

            try
            {
                // Use OEM as ID and call Product accessor (DAL)
                wasProductDeleted = _DbProductAccess.Delete(OEM);
            }
            // TODO: implement proper exception when Product was not deleted...
            catch (Exception ex)
            {
                wasProductDeleted = false;
                Debug.WriteLine(ex.Message);
            }
            return wasProductDeleted;
        }

        public IEnumerable<Product> GetAll()
        {
            IEnumerable<Product> allProductsInDB = new List<Product>();

            try
            {
                allProductsInDB = _DbProductAccess.GetAll();
            }
            // TODO: implement proper exception when list of all products is not returned (null, empty list etc.)...
            catch (Exception ex)
            {
                allProductsInDB = null;
                Debug.WriteLine(ex.Message);
            }
            return allProductsInDB;
        }

        public Product GetById(string OEM)
        {
            Product foundProductPlaceholder = null;

            try
            {
                foundProductPlaceholder = _DbProductAccess.GetById(OEM);
            }
            // TODO: implement proper exception when unique Product was not found...
            catch (Exception ex)
            {
                foundProductPlaceholder = null;
                Debug.WriteLine(ex.Message);
            }
            return foundProductPlaceholder;
        }

        public bool Update(Product entity)
        {
            bool wasProductUpdated;
            int numberOfRowsUpdated;

            try
            {
                numberOfRowsUpdated = _DbProductAccess.Update(entity);
                wasProductUpdated = (numberOfRowsUpdated == 1);
            }
            catch (Exception ex)
            {
                wasProductUpdated =  false;
                Debug.WriteLine(ex.Message);
            }
            return wasProductUpdated;
        }
    }
}
