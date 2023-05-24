//Dynamic strategy
using System.Text;

var tp = new TextProcessor();
tp.setOutPutFormat(Outputformat.Markdown);
tp.AppendList(new[] { "foo", "bar", "baz" });
Console.WriteLine(tp);

tp.setOutPutFormat(Outputformat.Html);
tp.Clear();
tp.AppendList(new[] { "foo", "bar", "baz" });
Console.WriteLine(tp);


public class TextProcessor
{
    private StringBuilder sb = new StringBuilder();
    private IListStrategy listStrategy;

    public void setOutPutFormat(Outputformat outputformat)
    {
        switch(outputformat)
        {
            case Outputformat.Markdown:
                listStrategy = new MarkdownlistStrategy();
                break;
            case Outputformat.Html:
                listStrategy = new HtmlListStrategy();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void AppendList(IEnumerable<string> items)
    {
        listStrategy.Start(sb);

        foreach (var item in items)
        {
            listStrategy.AddListItem(sb, item);
        }

        listStrategy.End(sb);
    }

    public StringBuilder Clear()
    {
        return sb.Clear();
    }

    public override string ToString()
    {
        return sb.ToString();
    }
}



public enum Outputformat
{
    Markdown,
    Html
}

public interface IListStrategy
{
    void Start(StringBuilder sb);
    void End(StringBuilder sb);
    void AddListItem(StringBuilder sb, string item);
}

public class HtmlListStrategy : IListStrategy
{

    public void Start(StringBuilder sb)
    {
        sb.AppendLine("<ul>");
    }
    public void End(StringBuilder sb)
    {
        sb.AppendLine("</ul>");
    }
    public void AddListItem(StringBuilder sb, string item)
    {
        sb.AppendLine($"    <li>{item}</li>");
    }
}

public class MarkdownlistStrategy : IListStrategy
{

    public void Start(StringBuilder sb)
    {

    }

    public void End(StringBuilder sb)
    {

    }

    public void AddListItem(StringBuilder sb, string item)
    {
        sb.AppendLine($" * {item}");
    }


}
