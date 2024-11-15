namespace RazorPagesToMVCMigration.Models
{
    public class Product
    {
        public string? OEM { get; set; }
        public string? VINNumber { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public DateTime DateAvailable { get; set; }
        public string? Notes { get; set; }
    }
}
