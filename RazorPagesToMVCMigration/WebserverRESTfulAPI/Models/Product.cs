namespace ServiceAPI.Models
{
    public class Product
    {
        public string? OEM { get; set; }
        public string? VINNumber { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAvailable { get; set; }
        public string? Notes { get; set; }

        public Product()
        {
            OEM = string.Empty;
            VINNumber = string.Empty;
            Name = "ProductNameGoesHere";
            Price = 0;
            DateAvailable = DateTime.UnixEpoch;
            Notes = string.Empty;
        }

        public Product(string oem, string vin, string name, decimal price, DateTime dt, string notes)
        {
            OEM = oem;
            VINNumber = vin;
            Name = name;
            Price = price;
            DateAvailable = dt;
            Notes = notes;
        }
    }
}
