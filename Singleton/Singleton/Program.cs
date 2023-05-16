var s1 = SingletonDatabase.Instance;
Console.WriteLine(s1.GetPopulation("Tokyo"));

Console.WriteLine();

public interface IDatabase
{
    int GetPopulation(string name);
}



public class SingletonDatabase : IDatabase
{

    private Dictionary<string, int> _capitals = new Dictionary<string, int>();

    //this way the instance is only initialized when someone tries to access it
    private static Lazy<SingletonDatabase> _instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

    public static SingletonDatabase Instance => _instance.Value;

    private SingletonDatabase()
    {
        //string filePath = "Capitals.txt";
        string[] lines = File.ReadAllLines("Capitals.txt");

        foreach (string line in lines)
        {
            string[] parts = line.Split('\t');
            string city = parts[0];
            //int population = int.Parse(parts[1]);
            _capitals.Add(city, 0);
        }
    }

    public int GetPopulation(string name)
    {
        return this._capitals[name];
    }

}

