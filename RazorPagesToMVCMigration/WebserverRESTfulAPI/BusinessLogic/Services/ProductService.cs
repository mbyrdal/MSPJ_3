using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Models;
using System.Diagnostics;

namespace ServiceAPI.BusinessLogic.Services
{
    public class ProductService : ICRUD<Product>
    {
        private readonly DbProduct _DbProductAccess;

        public ProductService(IConfiguration configuration)
        {
            _DbProductAccess = new DbProduct(configuration);
        }

        public bool Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string OEM)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetByID(string OEM)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
