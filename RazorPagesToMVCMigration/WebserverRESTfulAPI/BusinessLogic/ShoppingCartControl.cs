using System.Linq;
using ServiceAPI.Models;
using ServiceAPI.Utilities;

public class ShoppingCartControl
{
    private readonly ShoppingCart _cart;

    /*public ShoppingCartControl()
    {
        // Initialize cart
        _cart = SessionHelper.GetObjectFromJSON<ShoppingCart>("Cart") ?? new ShoppingCart();
    }

    public ShoppingCart GetCart()
    {
        return _cart;
    }

    public void AddToCart(Product product)
    {
        _cart.Items.Add(product);
        _cart.TotalPrice = _cart.Items.Sum(i => i.Price);
        SessionHelper.SetObjectAsJSON("Cart", _cart);
    }

    public void RemoveFromCart(int productId)
    {
        var product = _cart.Items.FirstOrDefault(i => i.ID == productId);
        if (product != null)
        {
            _cart.Items.Remove(product);
            _cart.TotalPrice = _cart.Items.Sum(i => i.Price);
            SessionHelper.SetObjectAsJSON("Cart", _cart);
        }
    }

    public void ClearCart()
    {
        _cart.Items.Clear();
        _cart.TotalPrice = 0;
        SessionHelper.SetObjectAsJSON("Cart", _cart);
    }*/
}
    