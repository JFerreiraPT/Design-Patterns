//var creatures = new Creature[100];


//We are going to use proxy to creatures to optimize the access to variables and computed them.
//If we have arrays with only ages, x, and another with y it would be a lot faster
//foreach (var c in creatures)
//{
//    c.X++;
//}

var creaturesOptimized = new Creatures(100); //Alocate memory for every creature

foreach (var c in creaturesOptimized)
{
    //We are referencing creatures proxy
    c.X++;
}

class Creatures
{
    private readonly int size;
    private byte[] age;
    private int[] x, y;

    public Creatures(int size)
    {
        this.size = size;
        age = new byte[size];
        x = new int[size];
        y = new int[size];
    }

    //this is just a placeholder to access a particular element
    public struct CreatureProxy
    {
        private readonly Creatures creatures;
        private readonly int index;

        public CreatureProxy(Creatures creatures, int index)
        {
            this.creatures = creatures;
            this.index = index;
        }
        public ref byte Age => ref creatures.age[index];
        public ref int X => ref creatures.x[index];
        public ref int Y => ref creatures.y[index];
    }

    public IEnumerator<CreatureProxy> GetEnumerator()
    {
        for (int pos = 0; pos < size; ++pos)
        {
            //This yiel means that this is not being created now, only we its really iterated
            //So when on for loop the program will come back here to create the object needed.
            //We lazy interate the list
            yield return new CreatureProxy(this, pos);
        }
    }
}

//class Creature
//{
//    public byte Age;
//    public int X, Y;
//}