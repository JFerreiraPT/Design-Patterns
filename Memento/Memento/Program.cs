var ba = new BankAccount(100);
var m1 = ba.Deposit(50);
var m2 = ba.Deposit(1000);

Console.WriteLine(ba);

ba.Undo();
Console.WriteLine(ba);
ba.Undo();
Console.WriteLine(ba);

public class Memento
{
    public int Balance { get; }

    public Memento(int balance)
    {
        Balance = balance;
    }
}

public class BankAccount
{
    private int balance;
    private List<Memento> changes = new List<Memento>();
    //indicates de current memento we are on
    private int current;

    public BankAccount(int balance)
    {
        this.balance = balance;
        changes.Add(new Memento(balance));
    }

    //Every deposit we return a memento
    public Memento Deposit(int amount)
    {
        balance += amount;
        var m = new Memento(balance);
        changes.Add(m);
        current++;
        return m;
    }

    public Memento Restore(Memento m)
    {
        if(m != null)
        {
            balance = m.Balance;
            changes.Add(m);
            current++;
            return m;
        }
        return null;
    }

    public Memento Undo()
    {

        if(current > 0)
        {
            var m = changes[--current];
            balance = m.Balance;
            return m;
        }
        return null;
    }

    public Memento redo()
    {
        if(current + 1 < changes.Count)
        {
            var m = changes[++current];
            balance = m.Balance;
            return m;
        }
        return null;
    }

    public override string ToString()
    {
        return $"Balance: {balance}";
    }
}