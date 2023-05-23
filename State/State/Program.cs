var ls = new Switch();

ls.On();
ls.Off();
ls.Off();


public class Switch
{
    //Instead of an object be just an int it is a complete state
    public State State = new OffState();
    public void On() {
        State.On(this);
    }
    public void Off() { State.Off(this); }
}



public abstract class State
{
    public virtual void On(Switch sw)
    {
        Console.WriteLine("Light is already on.");
    }
    public virtual void Off(Switch sw)
    {
        Console.WriteLine("Light is already off.");
    }
}

public class OnState : State
{
    public OnState()
    {
        Console.WriteLine("Light turned on");
    }

    public override void Off(Switch sw)
    {
        Console.WriteLine("Turning the light off...");
        sw.State = new OffState();
    }
}

public class OffState : State
{
    public override void On(Switch sw)
    {
        Console.WriteLine("Turning the light on...");
        sw.State = new OnState();
    }
    public OffState()
    {
        Console.WriteLine("Light turned off");
    }
}