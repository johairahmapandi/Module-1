using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DictionaryRepository<TKey, TValue> where TKey : IComparable<TKey>
{
    private Dictionary<TKey, TValue> _items = new Dictionary<TKey, TValue>();

    public void Add(TKey id, TValue item)
    {
        if (_items.ContainsKey(id))
        {
            Console.WriteLine("Error: An item with the same key already exists.");
            return;
        }
        _items.Add(id, item);
        Console.WriteLine("Item added successfully.");
    }

    public TValue Get(TKey id)
    {
        if (_items.ContainsKey(id))
            return _items[id];

        throw new KeyNotFoundException("Error: Item not found.");
    }

    public void Update(TKey id, TValue newItem)
    {
        if (!_items.ContainsKey(id))
        {
            Console.WriteLine("Error: Item not found.");
            return;
        }
        _items[id] = newItem;
        Console.WriteLine("Item updated successfully.");
    }

    public void Delete(TKey id)
    {
        if (_items.Remove(id))
            Console.WriteLine("Item removed successfully.");
        else
            Console.WriteLine("Error: Item not found.");
    }
}

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }

    public override string ToString()
    {
        return $"ProductId: {ProductId}, ProductName: {ProductName}";
    }
}

public class Program
{
    public static void Main()
    {
        var repository = new DictionaryRepository<int, Product>();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Get Product");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. Exit");
            Console.Write("\nChoice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Product ID: ");
                    int addId = int.Parse(Console.ReadLine());
                    Console.Write("Enter Product Name: ");
                    string addName = Console.ReadLine();
                    repository.Add(addId, new Product { ProductId = addId, ProductName = addName });
                    break;

                case 2:
                    Console.Write("Enter Product ID to retrieve: ");
                    int getId = int.Parse(Console.ReadLine());
                    try
                    {
                        var product = repository.Get(getId);
                        Console.WriteLine($"Product Found: {product}");
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case 3:
                    Console.Write("Enter Product ID to update: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Enter New Product Name: ");
                    string updateName = Console.ReadLine();
                    repository.Update(updateId, new Product { ProductId = updateId, ProductName = updateName });
                    break;

                case 4:
                    Console.Write("Enter Product ID to delete: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    repository.Delete(deleteId);
                    break;

                case 5:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
