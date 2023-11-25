using System;

// Abstract class representing a bank
public abstract class Bank
{
    public abstract void SendTransfer(double amount, Bank recipient);
    public abstract void ReceiveTransfer(double amount, Bank sender);
    public abstract double GetBalance();
}

//here i build class representing a basic account
public class Account : Bank
{
    private double balance;

    public Account(double initialBalance)
    {
        balance = initialBalance;
    }

    public override void SendTransfer(double amount, Bank recipient)
    {
        if (balance >= amount)
        {
            balance -= amount;
            recipient.ReceiveTransfer(amount, this);
            Console.WriteLine($"Transfer of ${amount} sent to {recipient}.");
        }
        else
        {
            Console.WriteLine("Insufficient funds to make the transfer.");
        }
    }

    public override void ReceiveTransfer(double amount, Bank sender)
    {
        balance += amount;
        Console.WriteLine($"Received a transfer of ${amount} from {sender}.");
    }

    public override double GetBalance()
    {
        return balance;
    }

    public override string ToString()
    {
        return $"Account with balance ${balance}";
    }
}

// here i build a class representing a smart account with some features
public class SmartAccount : Account
{
    private int rewardsPoints;

    public SmartAccount(double initialBalance, int initialRewardsPoints) : base(initialBalance)
    {
        rewardsPoints = initialRewardsPoints;
    }

    public override void ReceiveTransfer(double amount, Bank sender)
    {
        base.ReceiveTransfer(amount, sender);
        rewardsPoints += (int)(amount * 0.1); // 10% of the transfer as rewards points
        Console.WriteLine($"Earned {amount * 0.1} rewards points. Total points: {rewardsPoints}");
    }
}

class Program
{
    static void Main()
    {
        Bank account1 = new Account(1000);
        Bank account2 = new SmartAccount(500, 50);

        account1.SendTransfer(200, account2);
        Console.WriteLine(account1);
        Console.WriteLine(account2);

        account2.SendTransfer(50, account1);
        Console.WriteLine(account1);
        Console.WriteLine(account2);
    }
}
