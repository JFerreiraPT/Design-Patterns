//Protection Proxy

//Usage
ICar car = new CarProxy(new Driver(12));

//And exception will be thrown
car.Drive();


public interface ICar
{
    void Drive();
}

public class Car : ICar
{
    public void Drive()
    {
        Console.WriteLine("Car is being driven");
    }
}


public class CarProxy : ICar
{
    private Driver driver;
    //instatiation of class implementation is done here

    private Car car = new Car();

    public CarProxy(Driver driver)
    {
        this.driver = driver;
    }

    public void Drive()
    {
        if (driver.Age <= 17)
            throw new ArgumentException("To Young to drive");

        car.Drive();

    }
}


public class Driver
{
    public int Age { get; set; }

    public Driver(int age)
    {
        Age = age;
    }
}