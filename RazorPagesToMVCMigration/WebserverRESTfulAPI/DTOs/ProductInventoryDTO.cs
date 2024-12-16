using ServiceAPI.Models;

namespace ServiceAPI.DTOs

{
    public class ProductInventoryDTO : ProductDTO
    {
        public string Name { get; set; } = string.Empty;

        public ProductInventoryDTO()
        {
        }

        public ProductInventoryDTO(int ID, int carPartID, int carID, string name, string oem, decimal price, DateTime dt, string cond, string itemDesc, bool itemAvailable) : base(ID, carPartID, carID, oem, price, dt, cond, itemDesc, itemAvailable)
        {
            this.ID = ID;
            CarPartID = carPartID;
            CarID = carID;
            Name = name;
            OEM = oem;
            Price = price;
            DateAvailable = dt;
            Condition = cond;
            ItemDescription = itemDesc;
            ItemAvailable = itemAvailable;
        }
    }
}
