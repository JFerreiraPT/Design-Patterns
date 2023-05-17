using System.Text;

var drawing = new GraphicObject { Name = "My drawing" };
drawing.Children.Add(new Square { Color = "Red" });
drawing.Children.Add(new Circle { Color = "Yellow" });


var group = new GraphicObject();
group.Children.Add(new Square { Color = "Blue" });
group.Children.Add(new Circle { Color = "Blue" });

drawing.Children.Add(group);

Console.WriteLine(drawing);




public class GraphicObject
{
    public virtual string Name { get; set; } = "Group";
    public string Color;

    private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
    //Expose children, using lazy means that only when need the list will be instatiated
    public List<GraphicObject> Children => children.Value;


    //Build a composite tree of objects
    private void Print(StringBuilder sb, int depth)
    {
        sb.Append(new string('*', depth))
            .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
            .AppendLine(Name);

        foreach (var child in Children)
        {
            child.Print(sb, depth + 1);
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        //print top level
        Print(sb, 0);
        return sb.ToString();
    }
}

public class Circle : GraphicObject
{
    public override string Name => "Circle";
}

public class Square : GraphicObject
{
    public override string Name => "Square";
}