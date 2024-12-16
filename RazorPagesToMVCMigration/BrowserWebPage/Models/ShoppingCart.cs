using ServiceAPI.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BrowserWebPage.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ID { get; set; }

        [Key]

        public int ItemDescription { get; set; }

        [Required]
        public List<ProductInventoryViewModel> Items { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public ShoppingCart()
        {
            Items = new List<ProductInventoryViewModel>();
            TotalPrice = 0;
        }

        public ShoppingCart(List<ProductInventoryViewModel> items)
        {
            Items = items;
            TotalPrice = items.Sum(item => item.Price);
        }

        public ShoppingCart(List<ProductInventoryViewModel> items, decimal totalPrice)
        {
            Items = items;
            TotalPrice = totalPrice;
        }
    }
}
