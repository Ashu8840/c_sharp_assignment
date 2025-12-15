using System;

public delegate decimal DiscountStrategy(decimal price);

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }

    public Product(int id, string name, decimal price)
    {
        ProductId = id;
        ProductName = name;
        ProductPrice = price;
    }
}

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerType { get; set; }

    public Customer(int id, string name, string type)
    {
        CustomerId = id;
        CustomerName = name;
        CustomerType = type;
    }
}

public class Shopping
{
    public decimal FestivalDiscount(decimal price)
    {
        return price * 0.17m;
    }

    public decimal PremiumDiscount(decimal price)
    {
        return price * 0.28m;
    }

    public decimal ApplyDiscount(Product product, Customer customer)
    {
        DiscountStrategy discount;

        if (customer.CustomerType.ToLower() == "occasional")
        {
            discount = FestivalDiscount;
        }
        else if (customer.CustomerType.ToLower() == "regular")
        {
            discount = PremiumDiscount;
        }
        else
        {
            return product.ProductPrice;
        }

        decimal discountAmount = discount(product.ProductPrice);
        decimal finalPrice = product.ProductPrice - discountAmount;

        Console.WriteLine($"Product: {product.ProductName}");
        Console.WriteLine($"Customer: {customer.CustomerName} ({customer.CustomerType})");
        Console.WriteLine($"Original Price: ${product.ProductPrice}");
        Console.WriteLine($"Discount Amount: ${discountAmount}");
        Console.WriteLine($"Final Price: ${finalPrice}");
        Console.WriteLine();

        return finalPrice;
    }
}

public class Program
{
    public static void Main()
    {
        Product product1 = new Product(1, "Laptop", 1000);
        Product product2 = new Product(2, "Smartphone", 500);

        Customer customer1 = new Customer(101, "Arjun", "occasional");
        Customer customer2 = new Customer(102, "Priya", "regular");

        Shopping shopping = new Shopping();

        shopping.ApplyDiscount(product1, customer1);
        shopping.ApplyDiscount(product2, customer2);
    }
}
