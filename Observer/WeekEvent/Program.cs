var btn = new Button();
var window = new Window(btn);

var windowRef = new WeakReference(window);

btn.Fire();

Console.WriteLine("Setting window to null");
//usually setting window to null the destructor would be called, but
//when an event is sibscribed that is not what happens
window = null;


Util.FireGC();
Console.WriteLine($"Is the window alive after cg? { windowRef.IsAlive}");
//It will be alive!! so it means the is a leak

public class Button
{
    public event EventHandler Clicked;

    public void Fire()
    {
        Clicked?.Invoke(this, EventArgs.Empty);
    }

}


public class Window
{
    public Window(Button btn)
    {
        //subscribe to the event
        btn.Clicked += ButtonClicked;
    }

    private void ButtonClicked(object? sender, EventArgs e)
    {
        Console.WriteLine("Button clicked (window handler)");
    }

    //distructor
    ~Window()
    {
        Console.WriteLine("Window finalized");
    }
}

public class Util
{

    public static void FireGC()
    {
        Console.WriteLine("Starting GC");
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        Console.WriteLine("GC is done");

    }
}
