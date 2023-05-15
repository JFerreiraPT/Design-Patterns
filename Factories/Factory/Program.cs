var point = Point.Factory.NewCartesianPoint(1.0, Math.PI / 2);

public class Point
{
    private double x, y;

    //This is a property, any type called a new object is created
    public static Point Origin => new Point(0, 0); // This is a

    //Singleton field
    //This is a field, so it's only instatiated once
    public static Point Origin2 = new Point(0, 0); //Better

    //Private so we are no longer able to call it from exterior.
    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }


    //Factory fubction its just a separated class that instatiate Instances of objects to us
    public static class Factory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }

}