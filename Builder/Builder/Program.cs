using System.Text;


var builder = new HtmlBuilder("ul");
builder.AddChild("li", "Hello").AddChild("li", "world");
Console.WriteLine(builder.ToString());










public class HtmlElement
{
    public string Name, Text;
    public List<HtmlElement> Elements = new List<HtmlElement>();
    private const int identSize = 2;

    public HtmlElement()
    {
    }

    public HtmlElement(string name, string text)
    {
        Name = name;
        Text = text;
    }

    private string ToStringImpl(int indent)
    {
        var sb = new StringBuilder();
        var i = new string(' ', identSize * indent);
        sb.AppendLine($"{i}<{Name}>");

        if(!string.IsNullOrWhiteSpace(Text))
        {
            sb.Append(new string(' ', identSize * (indent + 1)));
            sb.AppendLine(Text);
        }


        foreach (var e in Elements)
        {
            sb.Append(e.ToStringImpl(identSize + 1));
            
        }

        sb.AppendLine($"{i}</{Name}>");

        return sb.ToString();
    }

    public override string ToString()
    {
        return ToStringImpl(0);
    }

}

public class HtmlBuilder
{
    HtmlElement root = new HtmlElement();
    private string _rootName;

    public HtmlBuilder(string rootName)
    {
        this._rootName = rootName;
        root.Name = rootName;
    }

    public HtmlBuilder AddChild(string childName, string childText)
    {
        var e = new HtmlElement(childName, childText);
        root.Elements.Add(e);
        return this;
    }

    public override string ToString()
    {
        return root.ToString();
    }

    public void Clear()
    {
        root = new HtmlElement { Name = _rootName };
    }
}

