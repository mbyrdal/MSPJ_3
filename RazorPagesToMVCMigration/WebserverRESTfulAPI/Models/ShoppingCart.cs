using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ServiceAPI.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Items in the shopping cart are required.")]
        public List<Product> Items { get; set; }

        [Required(ErrorMessage = "Total price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a non-negative value.")]
        public decimal TotalPrice { get; set; }

        public ShoppingCart()
        {
            Items = new List<Product>();
            TotalPrice = 0;
        }

        public ShoppingCart(List<Product> items)
        {
            Items = items ?? new List<Product>();
            TotalPrice = Items.Sum(item => item.Price);
        }

        public ShoppingCart(List<Product> items, decimal totalPrice)
        {
            Items = items ?? new List<Product>();
            TotalPrice = totalPrice;
        }

        /// <summary>
        /// Adds a product to the shopping cart.
        /// </summary>
        public void AddProduct(Product product)
        {
            if (product != null)
            {
                Items.Add(product);
                TotalPrice += product.Price;
            }
        }

        /// <summary>
        /// Removes a product from the shopping cart by its ID.
        /// </summary>
        public bool RemoveProduct(int productId)
        {
            var product = Items.FirstOrDefault(item => item.ID == productId);
            if (product != null)
            {
                Items.Remove(product);
                TotalPrice -= product.Price;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Clears all items from the shopping cart.
        /// </summary>
        public void ClearCart()
        {
            Items.Clear();
            TotalPrice = 0;
        }
    }
}
