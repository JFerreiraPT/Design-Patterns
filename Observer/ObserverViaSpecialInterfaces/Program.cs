public class Event
{

}

public class FallsIllEvent : Event
{
    public string Address;
}


public class Person : IObservable<Event>
{
    private readonly HashSet<Subscription> subscriptions
        = new HashSet<Subscription>();


    //No problem of likings, because the link will brake
    public IDisposable Subscribe(IObserver<Event> observer)
    {
        var subscription = new Subscription(this, observer);
        subscriptions.Add(subscription);
        return subscription;
    }

    public void FallIll()
    {
        //call the oberver of that subscription and call next
        foreach (var s in subscriptions)
        {
            s.Observer.OnNext(new FallsIllEvent { Address = "rua central Leiria" });
        }
    }

    private class Subscription : IDisposable
    {
        private readonly Person person;
        public readonly IObserver<Event> Observer;

        //1-> the object we are subscribing too
        //2-> components able to receive
        public Subscription(Person person, IObserver<Event> observer)
        {
            this.person = person;
            this.Observer = observer;

        }

        public void Dispose()
        {
            person.subscriptions.Remove(this);
        }
    }
}


//The class that will observe

public class Program : IObserver<Event>
{
    public static void Main(string[] args)
    {
        new Program();
    }

    public Program()
    {
        var person = new Person();
        using IDisposable sub = person.Subscribe(this);

        //trigger the event
        person.FallIll();

    }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    //This is where processing will happen
    public void OnNext(Event value)
    {
        if(value is FallsIllEvent args)
        {
            Console.WriteLine($"A doctor is required at {args.Address}");
        }
    }
}
