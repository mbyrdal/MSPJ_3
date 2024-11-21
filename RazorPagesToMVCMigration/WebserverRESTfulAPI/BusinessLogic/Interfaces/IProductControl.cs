using ServiceAPI.Models;
namespace ServiceAPI.BusinessLogic.Interfaces
{
    public interface IProductControl
    {
        Product GetProductByOEM(string OEM);
        List<Product> GetAllProducts();
        bool AddProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(string OEM);
    }
}
