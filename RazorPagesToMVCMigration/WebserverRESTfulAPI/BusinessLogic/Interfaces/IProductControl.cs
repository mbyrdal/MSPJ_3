using ServiceAPI.DTOs;
using ServiceAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface IProductControl
    {
        // CRUD Operations
        Task<Product> GetProductByIDAsync(int ID);
        Task<Product> GetProductByOEMAsync(string OEM);
        Task<List<Product>> GetAllProductsAsync();
        Task<bool> AddProductAsync(ProductViewModel productViewModel, string carPartName, string carVINNumber);
        Task<bool> UpdateProductAsync(string OEM, ProductViewModel product, string carPartName, string carVINNumber);
        Task<bool> DeleteProductAsync(string OEM);

        // Advanced Queries
        Task<List<Product>> GetProductsByConditionAsync(string condition);
        Task<List<Product>> GetProductsByCarPartNameAsync(string carPartName);
        Task<List<Product>> GetProductsByAvailabilityAsync(bool isAvailable);
    }
}
