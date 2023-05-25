using System.Text;

//Imagine that we dont have print available on Expression asbtract class!! The next example
// Refletive visitor will show how

var e = new AdditionalExpression(
    new DoubleExpression(1),
    new AdditionalExpression(
        new DoubleExpression(2),
        new DoubleExpression(3)
        ));
var sb = new StringBuilder();
e.Print(sb);
Console.WriteLine(sb);

public abstract class Expression
{
    public abstract void Print(StringBuilder sb);
}

public class DoubleExpression : Expression
{
    private double value;

    public DoubleExpression(double value)
    {
        this.value = value;
    }

    public override void Print(StringBuilder sb)
    {
        sb.Append(value);
    }
}

public class AdditionalExpression : Expression
{
    private Expression left, right;

    public AdditionalExpression(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override void Print(StringBuilder sb)
    {
        sb.Append("(");
        left.Print(sb);
        sb.Append("+");
        right.Print(sb);
        sb.Append(")");
    }
}
