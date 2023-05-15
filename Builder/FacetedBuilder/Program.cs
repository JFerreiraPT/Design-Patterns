var pb = new PersonBuilder();
Person person = pb
    .Works.At("Ki")
        .AsA("Developer")
        .Earning(1000000)
    .Lives.At("Colmeias")
          .WithPostalcode("123123")
          .InCity("Leiria");


Console.WriteLine(person);

public class Person
{
    public string StreetAddress, Postcode, City;

    public string CompanyName, Position;
    public int AnnualIncome;

    public override string ToString()
    {
        return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}," +
            $"{nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}," +
            $"{nameof(AnnualIncome)}: {AnnualIncome}";
    }

}

public class PersonBuilder //facade
{
    protected Person person = new Person();

    public PersonJobBuilder Works => new PersonJobBuilder(person);
    public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

    //We are implicitly converting PersonBuilder to person, so we dont need build() method
    public static implicit operator Person(PersonBuilder pb)
    {
        return pb.person;
    }
}

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person)
    {
        this.person = person;
    }

    public PersonJobBuilder At(string companyName)
    {
        person.CompanyName = companyName;
        return this;
    }
    public PersonJobBuilder AsA(string position)
    {
        person.Position = position;
        return this;
    }
    public PersonJobBuilder Earning(int amount)
    {
        person.AnnualIncome = amount;
        return this;
    }
}

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person)
    {
        this.person = person;
    }

    public PersonAddressBuilder At(string streetAddress)
    {
        person.StreetAddress = streetAddress;
        return this;
    }

    public PersonAddressBuilder WithPostalcode(string postcode)
    {
        person.Postcode = postcode;
        return this;
    }

    public PersonAddressBuilder InCity(string city)
    {
        person.City = city;
        return this;
    }
}
