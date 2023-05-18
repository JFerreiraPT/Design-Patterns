var ba = new BankAccount();

var commands = new List<BankAccountCommand>
{
    new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
    new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 50)
};

//we are not executing commands yet, so at this point balance is zero
Console.WriteLine(ba);

foreach (var cm in commands)
{
    cm.Call();
}

Console.WriteLine(ba);

public class BankAccount
{
    private int balance;
    private int overdraftLimit = -500;

    public void Deposit(int amount)
    {
        balance += amount;
        Console.WriteLine($"Deposited {amount}€, balance is now {balance}€");
    }

    public void withdraw(int amount)
    {
        if(balance - amount >= overdraftLimit)
        {
            balance -= amount;
            Console.WriteLine($"Withdrew {amount}€, balance is now {balance}€");
        }
    }

    public override string ToString()
    {
        return $"Balance: {balance}";
    }
}

public interface ICommand
{
    void Call();
}

public class BankAccountCommand : ICommand
{
    private BankAccount account;
    public enum Action
    {
        Deposit, Withdraw
    }

    private Action action;
    private int amount;

    public BankAccountCommand(BankAccount account, Action action, int amount)
    {
        this.account = account;
        this.action = action;
        this.amount = amount;
    }

    public void Call()
    {
        switch (action)
        {
            case Action.Deposit:
                account.Deposit(amount);
                break;
            case Action.Withdraw:
                account.withdraw(amount);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
