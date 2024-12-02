using ServiceAPI.DTOs;
using ServiceAPI.Models;
namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface IProductControl
    {
        Product GetProductByID(int ID);
        Product GetProductByOEM(string OEM);
        List<Product> GetAllProducts();
        bool AddProduct(ProductViewModel productViewModel, string carPartName, string carVINNumber);
        bool UpdateProduct(string OEM, ProductViewModel product,  string carPartName, string carVINNumber);
        bool DeleteProduct(string OEM);
    }
}
