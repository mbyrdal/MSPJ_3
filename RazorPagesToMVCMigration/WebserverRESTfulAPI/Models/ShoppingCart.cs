using System.ComponentModel.DataAnnotations;

namespace ServiceAPI.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public List<Product> Items { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public ShoppingCart()
        {
            Items = new List<Product>();
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
