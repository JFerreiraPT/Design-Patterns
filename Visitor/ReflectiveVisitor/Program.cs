using System;
using System.Collections.Generic;
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
ExpressionPrinter.Print(e, sb);
Console.WriteLine(sb);




public abstract class Expression
{
    //How we implement print if no print method available on hierarchy?
    //public abstract void Print(StringBuilder sb);
}

public class DoubleExpression : Expression
{
    public double Value;

    public DoubleExpression(double value)
    {
        Value = value;
    }

    //How we implement print if no print method available on hierarchy?
    //public override void Print(StringBuilder sb)
    //{
    //    sb.Append(value);
    //}
}

public class AdditionalExpression : Expression
{
    public Expression Left, Right;

    public AdditionalExpression(Expression left, Expression right)
    {
        Left = left;
        Right = right;
    }

    //How we implement print if no print method available on hierarchy?
    //public override void Print(StringBuilder sb)
    //{
    //    sb.Append("(");
    //    left.Print(sb);
    //    sb.Append("+");
    //    right.Print(sb);
    //    sb.Append(")");
    //}
}


//Implement print from exterior
//This way we break Open Closed principle, every time another Expression type is added we have to change this class
public static class ExpressionPrinter
{
    public static void Print(Expression e, StringBuilder sb)
    {
        if(e is DoubleExpression de)
        {
            sb.Append(de.Value);
        } else if(e is AdditionalExpression ae)
        {
            sb.Append("(");
            Print(ae.Left, sb);
            sb.Append("+");
            Print(ae.Right, sb);
            sb.Append(")");
        }
    }
}



//With a dictionary instead of swicth+/if

namespace DotNetDesignPatternDemos.Behavioral.Visitor.Reflective
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;

    public abstract class Expression
    {
    }

    public class DoubleExpression : Expression
    {
        public double Value;

        public DoubleExpression(double value)
        {
            Value = value;
        }
    }

    public class AdditionExpression : Expression
    {
        public Expression Left;
        public Expression Right;

        public AdditionExpression(Expression left, Expression right)
        {
            Left = left ?? throw new ArgumentNullException(paramName: nameof(left));
            Right = right ?? throw new ArgumentNullException(paramName: nameof(right));
        }
    }



    public static class ExpressionPrinter
    {
        private static DictType actions = new DictType
        {
            [typeof(DoubleExpression)] = (e, sb) =>
            {
                var de = (DoubleExpression)e;
                sb.Append(de.Value);
            },
            [typeof(AdditionExpression)] = (e, sb) =>
            {
                var ae = (AdditionExpression)e;
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");
            }
        };

        public static void Print2(Expression e, StringBuilder sb)
        {
            actions[e.GetType()](e, sb);
        }

        public static void Print(Expression e, StringBuilder sb)
        {
            if (e is DoubleExpression de)
            {
                sb.Append(de.Value);
            }
            else
            if (e is AdditionExpression ae)
            {
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");
            }
            // breaks open-closed principle
            // will work incorrectly on missing case
        }
    }

    public class Demo
    {
        public static void Main()
        {
            var e = new AdditionExpression(
              left: new DoubleExpression(1),
              right: new AdditionExpression(
                left: new DoubleExpression(2),
                right: new DoubleExpression(3)));
            var sb = new StringBuilder();
            ExpressionPrinter.Print2(e, sb);
            Console.WriteLine(sb);
        }
    }
}