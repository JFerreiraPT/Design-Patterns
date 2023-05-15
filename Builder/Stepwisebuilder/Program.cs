var car = CarBuilder.Create()
    .IfType(CarType.Crossover)
    .WithWheels(18)
    .Build();

Console.WriteLine(car);


public enum CarType
{
    Sedan,
    Crossover
}

//We will need a builder that allows the creation of a type and only after the wheelSize
//We will use interface segregation principle from SOLID
public class Car
{
    public CarType Type;
    public int WheelSize;

    public override string ToString()
    {
        return $"Car: {Type} with wheel size: {WheelSize}";
    }
}

public interface ISpecifyCarType
{
    //This is the order we want to return the this keyword in order to chain constructor methods
    ISpecifyWheelSize IfType(CarType type);
}

public interface ISpecifyWheelSize
{
    IBuildCar WithWheels(int size);
}

public interface IBuildCar
{
    public Car Build();
}

public class CarBuilder
{  

    private class Impl :
        ISpecifyCarType,
        ISpecifyWheelSize,
        IBuildCar
    {

        private Car car = new Car();


        public ISpecifyWheelSize IfType(CarType type)
        {
            car.Type = type;
            return this;
        }

        public IBuildCar WithWheels(int size)
        {
            switch (car.Type)
            {
                case CarType.Crossover when size < 17 || size > 20:
                case CarType.Sedan when size < 15 || size > 17:
                    throw new ArgumentException($"Wrong size of wheel of {car.Type}.");

            }
            car.WheelSize = size;
            return this;
        }
        public Car Build()
        {
            return car;
        }
    }


    public static ISpecifyCarType Create()
    {
        return new Impl();
    }
}