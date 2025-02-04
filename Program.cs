using System;
using LibraryEcommerce.Services;

class Program
{
    static void Main()
    {
        var service = new ProductServProxy();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nMenu:\n1. Add Product\n2. View Inventory\n3. Update Product\n4. Add to Cart\n5. View Cart\n6. Remove from Cart\n7. Checkout & Exit");
            Console.Write("Choose an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter price: ");
                    decimal price = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Enter quantity: ");
                    int quantity = Convert.ToInt32(Console.ReadLine());
                    service.CreateProduct(name, price, quantity);
                    break;

                case "2":
                    foreach (var p in service.GetInventory())
                        Console.WriteLine($"{p.Id}: {p.Name} - ${p.Price} ({p.Quantity} left)");
                    break;

                case "3":
                    Console.Write("Enter Product ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter new name: ");
                    name = Console.ReadLine();
                    Console.Write("Enter new price: ");
                    price = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Enter new quantity: ");
                    quantity = Convert.ToInt32(Console.ReadLine());
                    service.UpdateProduct(id, name, price, quantity);
                    break;

                case "4":
                    Console.Write("Enter Product ID to add to cart: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter quantity: ");
                    quantity = Convert.ToInt32(Console.ReadLine());
                    service.AddToCart(id, quantity);
                    break;

                case "5":
                    foreach (var c in service.GetCart())
                        Console.WriteLine($"{c.Id}: {c.Name} - {c.Quantity} in cart");
                    break;

                case "6":
                    Console.Write("Enter Product ID to remove from cart: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    service.RemoveFromCart(id);
                    break;

                case "7":
                    service.Checkout();
                    running = false;
                    break;
            }
        }
    }
}

