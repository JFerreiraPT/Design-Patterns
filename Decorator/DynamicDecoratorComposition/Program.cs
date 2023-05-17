var square = new Square(1.23f);
Console.WriteLine(square.AsString());

var redSquare = new ColorShape(square, "red");

Console.WriteLine(redSquare.AsString());

var transparentRedSquare = new TransparentShape(redSquare, 0.7f);
Console.WriteLine(transparentRedSquare.AsString());

public interface IShape
{
    string AsString();
}

public class Circle : IShape
{
    private float radius;

    public Circle(float radius)
    {
        this.radius = radius;
    }

    public string AsString() => $"A circle with radius {radius}";

    public void Resize(float factor)
    {
        radius *= factor;
    }
}

public class Square : IShape
{ 
    private float side;

    public Square(float size)
    {
        this.side = size;
    }

    public string AsString() => $"A square with side {side}";
}

//Now lets build a decorator!
//They will ne dynamic! runned at running time instead of compiling!
//this can be attached to the object
public class ColorShape :IShape
{
    private IShape shape;
    private string color;

    public ColorShape(IShape shape, string color)
    {
        this.shape = shape;
        this.color = color;
    }

    public string AsString() => $"{shape.AsString()} has the color {color}";
}

public class TransparentShape : IShape
{
    private IShape shape;
    private float transparency;

    public TransparentShape(IShape shape, float transparency)
    {
        this.shape = shape;
        this.transparency = transparency;
    }

    public string AsString()
    {
        return $"{shape.AsString()} has transparency {transparency}";
    }
}
