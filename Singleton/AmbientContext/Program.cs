
using System.Text;

var house = new Building();



//gnd 3000
//Inside this scope it will be 3000
using (new BuildingContext(3000))
{
    house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
    house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));

    //1st 3500
    //We can create a new context
    using (new BuildingContext(3500))
    {
        house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
        house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 4000)));
    }

    //After this the context will be 3000 again!
    house.Walls.Add(new Wall(new Point(5000, 0), new Point(5000, 4000)));

}

Console.WriteLine(house);

//This building context should be singleton, and its not thread safe!
public sealed class BuildingContext : IDisposable
{
    public int WallHeight;

    public static Stack<BuildingContext> stack = new Stack<BuildingContext>();

    static BuildingContext()
    {
        //Will enter only once when assembly is loaded
        Console.WriteLine("On static Constructor method");
        stack.Push(new BuildingContext(0));        
    }

    public static BuildingContext Current => stack.Peek();

    public BuildingContext(int wallHeight)
    {
        WallHeight = wallHeight;
        stack.Push(this);
    }

    public void Dispose()
    {
        if(stack.Count > 1)
        {
            stack.Pop();
        }
    }
}

public class Building
{
    public List<Wall> Walls = new List<Wall>();

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var wall in Walls)
        {
            sb.AppendLine(wall.ToString());

        }

        return sb.ToString();
    }
}

public class Wall
{
    public Point Start, End;
    public int Height;

    public Wall(Point start, Point end)
    {
        Start = start;
        End = end;
        Height = BuildingContext.Current.WallHeight;
    }

    public override string ToString()
    {
        return $"Start: {Start}, End: {End}, Height: {Height}";
    }
}

public struct Point
{
    private int x, y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"x: {x}, y: {y}";
    }
}