using LibraryEcommerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryEcommerce.Services;

public class ProductServProxy
{
    private List<Product> inventory = new();
    private List<Product> shoppingCart = new();
    private int nextId = 1;

    public void CreateProduct(string name, decimal price, int quantity)
    {
        inventory.Add(new Product { Id = nextId++, Name = name, Price = price, Quantity = quantity });
    }

    public List<Product> GetInventory() => inventory;
    public List<Product> GetCart() => shoppingCart;

    public bool UpdateProduct(int id, string name, decimal price, int quantity)
    {
        var product = inventory.FirstOrDefault(p => p.Id == id);
        if (product == null) return false;
        product.Name = name;
        product.Price = price;
        product.Quantity = quantity;
        return true;
    }

    public bool AddToCart(int id, int quantity)
    {
        var product = inventory.FirstOrDefault(p => p.Id == id);
        if (product == null || product.Quantity < quantity) return false;

        product.Quantity -= quantity;
        var cartItem = shoppingCart.FirstOrDefault(p => p.Id == id);
        if (cartItem != null)
            cartItem.Quantity += quantity;
        else
            shoppingCart.Add(new Product { Id = id, Name = product.Name, Price = product.Price, Quantity = quantity });

        return true;
    }

    public bool RemoveFromCart(int id)
    {
        var cartItem = shoppingCart.FirstOrDefault(p => p.Id == id);
        if (cartItem == null) return false;

        var product = inventory.FirstOrDefault(p => p.Id == id);
        if (product != null)
            product.Quantity += cartItem.Quantity;

        shoppingCart.Remove(cartItem);
        return true;
    }

    public void Checkout()
    {
        decimal total = shoppingCart.Sum(p => p.Price * p.Quantity);
        decimal tax = total * 0.07m;
        decimal finalAmount = total + tax;

        Console.WriteLine("Receipt:");
        foreach (var item in shoppingCart)
            Console.WriteLine($"{item.Name} x{item.Quantity} - ${item.Price * item.Quantity}");
        Console.WriteLine($"Tax: ${tax}");
        Console.WriteLine($"Total: ${finalAmount}");

        shoppingCart.Clear();
    }
}
