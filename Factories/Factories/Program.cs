public class Point
{
    private double x, y;

    //Private so we are no longer able to call it from exterior.
    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    //When we want to instantiate Points we simple use the factory methods
    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }
}