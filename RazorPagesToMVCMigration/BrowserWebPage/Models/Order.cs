namespace BrowserWebPage.Models
{
    public class Order
    {
        public int ID { get; set; }
        public List<string>? ListOfProducts { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateSold { get; set; }
    }
}
