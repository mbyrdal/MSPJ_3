using BrowserWebPage.Models;

namespace BrowserWebPage.DTOs

{
    public class ProductInventoryViewModel : ProductViewModel
    {
        public string Name { get; set; } = string.Empty;

        public ProductInventoryViewModel()
        {
        }

        public ProductInventoryViewModel(int ID, int carPartID, int carID, string name, string oem, decimal price, DateTime dt, string cond, string itemDesc, bool itemAvailable) : base(ID, carPartID, carID, oem, price, dt, cond, itemDesc, itemAvailable)
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
