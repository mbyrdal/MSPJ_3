using ServiceAPI.DTOs;
using ServiceAPI.Models;
namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface IProductControl
    {
        Product GetProductByID(int ID);
        List<Product> GetAllProducts();
        bool AddProduct(Product product);
        bool AddProductDTO(ProductViewModel productViewModel);
        bool AddProductDTO(ProductViewModel productViewModel, string carPartName, string carVINNumber);
        bool UpdateProduct(Product product);
        bool UpdateProductDTO(ProductViewModel productViewModel);
        bool DeleteProduct(int ID);
    }
}
