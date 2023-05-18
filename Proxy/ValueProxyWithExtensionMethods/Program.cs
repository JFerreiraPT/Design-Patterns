Console.WriteLine(10f * 5.Percent());
Console.WriteLine(2.Percent() + 3.Percent());

//this will be a proxy for value
public struct Percentage
{
    private readonly float value;
    internal Percentage(float value)
    {
        this.value = value;
    }

    //overload the x operation
    public static float operator *(float f, Percentage p)
    {
        return f * p.value;
    }
    public static Percentage operator +(Percentage f, Percentage p)
    {
        return new Percentage(f.value + p.value);
    }

    public override string ToString()
    {
        return $"{value * 100}%";
    }
}

public static class PercentageExtensions
{
    //Extension methods, the this keyword defines the type where we can apply it
    public static Percentage Percent(this int value)
    {
        return new Percentage(value / 100.0f);
    }
    public static Percentage Percent(this float value)
    {
        return new Percentage(value / 100.0f);
    }
}

