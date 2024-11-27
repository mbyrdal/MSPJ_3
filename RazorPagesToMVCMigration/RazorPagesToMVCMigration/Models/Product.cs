namespace RazorPagesToMVCMigration.Models
{
    public class Product
    {
        public int ID { get; set; } // Add this property to fix the error
        public int CarPartID { get; set; }
        public int SaleID { get; set; }
        public string? OEM { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAvailable { get; set; }
        public string? Condition { get; set; }
        public string? ItemDescription { get; set; }
        public string? VINNumber { get; set; }
        public string? Name { get; set; }    
        public string? Notes { get; set; }
        
    }
}
