var ceo = new CEO();
ceo.Name = "João Ferreira";
ceo.Age = 34;

Console.WriteLine(ceo);

var ceo2 = new CEO();
//this changes will also afect ceo2
ceo.Name = "João P. R. Ferreira";

Console.WriteLine(ceo2);


public class CEO
{
    private static string name;
    private static int age;

    public string Name
    {
        get => name;
        set => name = value;
    }

    //Return static methods, this means we will have only 1 value for it!!
    //No matter how much instaces we have
    public int Age
    {
        get => age;
        set => age = value;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}";
    }
}