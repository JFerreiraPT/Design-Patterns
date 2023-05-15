
//person.New returns a builder
var me = Person.New.Called("João Ferreira").WorkAsA("Developer").Build();
Console.WriteLine(me);

public class Person
{
    public string Name;
    public string Position;

    public class Builder : PersonJobBuilder<Builder>
    {
        
    }

    public static Builder New => new Builder();

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}

public abstract class PersonBuilder
{
    protected Person person = new Person();
    public Person Build()
    {
        return person;
    }
}

public class PersonalInfoBuilder<SELF> : PersonBuilder
    where SELF : PersonalInfoBuilder<SELF>
{
    

    //Fluid method
    public SELF Called(string name)
    {
        person.Name = name;
        return (SELF)this;
    }
}

//this examples serves to show the problem with ihneritance
public class PersonJobBuilder<SELF> : PersonalInfoBuilder<PersonJobBuilder<SELF>>
    where SELF : PersonJobBuilder<SELF>
{
    public SELF WorkAsA(string postion)
    {
        person.Position = postion;
        return (SELF)this;
    }
}

