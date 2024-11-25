namespace ServiceAPI.Models
{
    public class ShoppingCart
    {
        public List<Product>? Items { get; set; }
        public decimal TotalPrice { get; set; }

        public ShoppingCart()
        {
            TotalPrice = 0;
        }

        public ShoppingCart(List<Product> items)
        {
            Items = items;
            TotalPrice = items.Sum(item => item.Price);
        }

        public ShoppingCart(List<Product> items, decimal totalPrice)
        {
            Items = items;
            TotalPrice = totalPrice;
        }
    }
}
