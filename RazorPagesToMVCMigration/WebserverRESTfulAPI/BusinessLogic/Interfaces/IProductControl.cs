using ServiceAPI.DTOs;
using ServiceAPI.Models;
namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface IProductControl
    {
        int GetProductID(string OEM);
        Product GetProductByID(int productID);
        string GetProductName(Product product);
        Product GetProduct(string OEM, string carPartName, string carVINNumber);
        List<Product> GetAllProducts();
        bool AddProduct(ProductViewModel productViewModel, string carPartName, string carVINNumber);
        bool UpdateProduct(string OEM, ProductViewModel product,  string carPartName, string carVINNumber);
        bool DeleteProduct(string OEM);
    }
}
