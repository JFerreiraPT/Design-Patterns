var john = new Employee();
john.Names = new[] { "John", "Doe" };
john.Address = new Address
{
    HouseNumber = 123,
    StreetName = "London Road"
};

john.Salary = 3210000;
var c1 = john.DeepCopy();

//this way we can easily chose the level on the hierarchy we want to copy
var copy = john.DeepCopy<Employee>();
var copy2 = john.DeepCopy<Person>();

copy.Names[1] = "Smith";
copy.Address.HouseNumber = 456;
copy.Salary = 123000;

Console.WriteLine(john);
Console.WriteLine(copy);
Console.WriteLine(copy2);



public interface IDeepCopyable<T>
    where T : new()
{
    void CopyTo(T target);

    public T DeepCopy()
    {
        T t = new T();
        CopyTo(t);
        return t;
    }
}

public class Address : IDeepCopyable<Address>
{
    public string StreetName;
    public int HouseNumber;

    public Address()
    {
    }

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }

    public void CopyTo(Address target)
    {
        target.StreetName = StreetName;
        target.HouseNumber = HouseNumber;
    }

    public override string ToString()
    {
        return $"Address: {StreetName} {HouseNumber}";
    }
}

public class Person : IDeepCopyable<Person>
{
    public string[] Names;
    public Address Address;

    public Person()
    {
    }

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public void CopyTo(Person target)
    {
        target.Names = (string[])Names.Clone();
        //target.Address = ((IDeepCopyable<Address>)Address).DeepCopy();
        target.Address = Address.DeepCopy();
    }

    public override string ToString()
    {
        return $"Names: {string.Join(",", Names)}, Address: {Address}";
    }
}

public class Employee : Person, IDeepCopyable<Employee>
{
    public int Salary;

    public Employee()
    {
    }

    public Employee(string[] names, Address address, int salary) : base(names, address)
    {
        Salary = salary; 
    }

    public void CopyTo(Employee target)
    {
        base.CopyTo(target);
        target.Salary = Salary;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Salary: {Salary}";
    }

}


public static class ExtensionMethods
{
    public static T DeepCopy<T>(this IDeepCopyable<T> item)
        where T : new()
    {
        return item.DeepCopy();
    }

    public static T DeepCopy<T>(this T person)
        where T : Person, new()
    {
        return ((IDeepCopyable<T>)person).DeepCopy();
    }
}