using System;

public delegate void PaymentProcessor(string cardNumber, decimal amount);

public class PaymentGateway
{
    public void ProcessCreditCard(string cardNumber, decimal amount)
    {
        Console.WriteLine($"Processing Credit Card Payment");
        Console.WriteLine($"Card Number: {cardNumber}");
        Console.WriteLine($"Amount: ${amount}");
        Console.WriteLine($"Status: Payment Processed Successfully via Credit Card");
        Console.WriteLine();
    }

    public void ProcessUPI(string upiId, decimal amount)
    {
        Console.WriteLine($"Processing UPI Payment");
        Console.WriteLine($"UPI ID: {upiId}");
        Console.WriteLine($"Amount: ${amount}");
        Console.WriteLine($"Status: Payment Processed Successfully via UPI");
        Console.WriteLine();
    }

    public void ProcessNetBanking(string bankAccount, decimal amount)
    {
        Console.WriteLine($"Processing NetBanking Payment");
        Console.WriteLine($"Bank Account: {bankAccount}");
        Console.WriteLine($"Amount: ${amount}");
        Console.WriteLine($"Status: Payment Processed Successfully via NetBanking");
        Console.WriteLine();
    }
}

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }

    public Customer(int id, string name)
    {
        CustomerId = id;
        CustomerName = name;
    }
}

public class Transaction
{
    public int TransactionId { get; set; }
    public Customer Customer { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }

    public Transaction(int id, Customer customer, decimal amount, string method)
    {
        TransactionId = id;
        Customer = customer;
        Amount = amount;
        PaymentMethod = method;
    }

    public void ProcessTransaction()
    {
        PaymentGateway gateway = new PaymentGateway();
        PaymentProcessor processor = null;

        Console.WriteLine($"Transaction ID: {TransactionId}");
        Console.WriteLine($"Customer: {Customer.CustomerName}");
        Console.WriteLine($"Amount: ${Amount}");
        Console.WriteLine($"Payment Method: {PaymentMethod}");
        Console.WriteLine();

        if (PaymentMethod.ToLower() == "creditcard")
        {
            processor = gateway.ProcessCreditCard;
            processor("4532-1234-5678-9012", Amount);
        }
        else if (PaymentMethod.ToLower() == "upi")
        {
            processor = (cardNumber, amount) => gateway.ProcessUPI("customer@upi", amount);
            processor("", Amount);
        }
        else if (PaymentMethod.ToLower() == "netbanking")
        {
            processor = (cardNumber, amount) => gateway.ProcessNetBanking("1234567890", amount);
            processor("", Amount);
        }
        else
        {
            Console.WriteLine("Invalid Payment Method");
            Console.WriteLine();
        }
    }
}

public class Program
{
    public static void Main()
    {
        Customer customer1 = new Customer(201, "Rahul");
        Customer customer2 = new Customer(202, "Anjali");
        Customer customer3 = new Customer(203, "Vikram");

        Transaction transaction1 = new Transaction(1001, customer1, 500, "creditcard");
        Transaction transaction2 = new Transaction(1002, customer2, 1500, "upi");
        Transaction transaction3 = new Transaction(1003, customer3, 2000, "netbanking");

        transaction1.ProcessTransaction();
        transaction2.ProcessTransaction();
        transaction3.ProcessTransaction();
    }
}
