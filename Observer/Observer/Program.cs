var p = new Person();
//subscribe to an event
p.FallsIll += CallDoctor;

//emit and event
p.CatchACold();

//if we dont want to listen to events anymore
p.FallsIll -= CallDoctor;

//Nothing will happen 
p.CatchACold();

void CallDoctor(object? sender, FallsIllEventArgs e)
{
    Console.WriteLine($"A doctor has been called to {e.Address}");
}

public class FallsIllEventArgs
{
    public string Address;
}

public class Person
{
    public void CatchACold()
    {
        FallsIll?.Invoke(this, new FallsIllEventArgs { Address = "123 London Road" });
    }

    public event EventHandler<FallsIllEventArgs> FallsIll;
}