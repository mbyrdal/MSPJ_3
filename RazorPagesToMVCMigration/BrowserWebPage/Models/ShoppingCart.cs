using ClientWeb.DTOs;
using System.ComponentModel.DataAnnotations;

namespace ClientWeb.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ID { get; set; }

        [Key]
        public int ItemDescription { get; set; }

        [Required]
        public List<ProductInventoryDTO> Items { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public ShoppingCart()
        {
            Items = new List<ProductInventoryDTO>();
            TotalPrice = 0;
        }

        public ShoppingCart(List<ProductInventoryDTO> items)
        {
            Items = items;
            TotalPrice = items.Sum(item => item.Price);
        }

        public ShoppingCart(List<ProductInventoryDTO> items, decimal totalPrice)
        {
            Items = items;
            TotalPrice = totalPrice;
        }
    }
}
