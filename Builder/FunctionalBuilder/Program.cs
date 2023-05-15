﻿var person = new PersonBuilder()
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

//Sealed prevents other classes from ingerit from it
public sealed class PersonBuilder
{
    private readonly List<Func<Person, Person>> actions
        = new List<Func<Person, Person>>();

    public PersonBuilder Called(string name)
        => Do(p => p.Name = name);

    public PersonBuilder Do(Action<Person> action) => AddAction(action);

    public Person Build()
        => actions.Aggregate(new Person(), (p, f) => f(p));

    private PersonBuilder AddAction(Action<Person> action)
    {
        actions.Add(p => { action(p);
            return p;
        });

        return this;
    }
}


public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
        => builder.Do(p => p.Position = position);
}