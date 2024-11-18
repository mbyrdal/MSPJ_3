namespace BrowserWebPage.Models
{
    public class Car
    {
        public string VINNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int ProductionYear { get; set; }
        public int Mileage { get; set; }
    }
}
