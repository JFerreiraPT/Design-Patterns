var person = new PersonBuilder()
    .Called("Sarah")
    .WorksAs("Developer")
    .Build();
Console.WriteLine(person);

public class Person
{
    public string Name, Position;

    public override string ToString()
    {
        return $"Name: {Name}, Position: {Position}";
    }
}


public abstract class FunctionalBuilder<TSubject, TSelf>
    where TSelf : FunctionalBuilder<TSubject, TSelf>
    //this means that TSubject should gave parameter-less constructor
    //this way we can di TSubject subject = new TSubject() otherwise it woul't be possible
    //We can combine multiple where T : class: new() -> should be a class and have an empty constructor
    where TSubject: new()
{
    private readonly List<Func<Person, Person>> actions
        = new List<Func<Person, Person>>();

    public TSelf Called(string name)
        => Do(p => p.Name = name);

    public TSelf Do(Action<Person> action) => AddAction(action);

    public Person Build()
        => actions.Aggregate(new Person(), (p, f) => f(p));

    private TSelf AddAction(Action<Person> action)
    {
        actions.Add(p =>
        {
            action(p);
            return p;
        });

        return (TSelf)this;
    }
}

public sealed class PersonBuilder
    : FunctionalBuilder<Person, PersonBuilder>
{

}


//Sealed prevents other classes from ingerit from it
//public sealed class PersonBuilder
//{
//    private readonly List<Func<Person, Person>> actions
//        = new List<Func<Person, Person>>();

//    public PersonBuilder Called(string name)
//        => Do(p => p.Name = name);

//    public PersonBuilder Do(Action<Person> action) => AddAction(action);

//    public Person Build()
//        => actions.Aggregate(new Person(), (p, f) => f(p));

//    private PersonBuilder AddAction(Action<Person> action)
//    {
//        actions.Add(p => { action(p);
//            return p;
//        });

//        return this;
//    }
//}


public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
        => builder.Do(p => p.Position = position);
}