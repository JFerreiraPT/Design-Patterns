Client c1 = new Client(new ElegantClothesFactory());
Console.WriteLine(c1.DescribeYourClothes());

Client c2 = new Client(new CasualClothesfactory());
Console.WriteLine(c2.DescribeYourClothes());


public class Shirt
{
}

public class Trousers
{
}

//PRODUCTS
public class DressShirt : Shirt
{
}

public class PoloShirt : Shirt
{
}

public class Jeans : Trousers
{
}

public class SuitTrousers : Trousers
{
}


public abstract class ClothesFactory
{
    public abstract Shirt CreateShirt();
    public abstract Trousers CreateTrousers();
}


public class CasualClothesfactory : ClothesFactory
{
    public override Shirt CreateShirt()
    {
        return new PoloShirt();
    }

    public override Trousers CreateTrousers()
    {
        return new Jeans();
    }
}

public class ElegantClothesFactory : ClothesFactory
{
    public override Shirt CreateShirt()
    {
        return new DressShirt();
    }

    public override Trousers CreateTrousers()
    {
        return new SuitTrousers();
    }
}

public class Client
{
    private readonly Shirt _shirt;
    private readonly Trousers _trousers;


    public Client(ClothesFactory factory)
    {
        _shirt = factory.CreateShirt();
        _trousers = factory.CreateTrousers();
    }

    public string DescribeYourClothes()
    {
        return $"Shirt: {_shirt} and Trousers: {_trousers}";
    }
}


