using RazorPagesToMVCMigration.Models;

namespace RazorPagesToMVCMigration.DAL.Interface
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductsByID(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        void Save();
    }
}
