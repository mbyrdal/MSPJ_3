using ServiceAPI.DTOs;
using ServiceAPI.Models;

namespace ServiceAPI.DatabaseAccess.Interfaces
{
    public interface IDbProduct
    {
        List<Product> GetAllEntities();
        Product GetByIdentifier(int id);
        Product GetByInputs(int id, int carPartID, int carID);
        int CreateEntity(ProductViewModel product, int carPartID, int carID);
        int UpdateEntity(ProductViewModel updateProduct, int productID, int carPartID, int carID);
        bool DeleteEntity(int id);
    }
}
