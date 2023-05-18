//Method chain

var goblin = new Creature("Goblin", 2, 2);
var root = new CreatureModifier(goblin);

root.Add(new DoubleAttackModifier(goblin));
root.Add(new IncreaseDefenseModifier(goblin));
root.Handle();

public class Creature
{
    public string Name;
    public int Attack, Defense;

    public Creature(string name, int attack, int defense)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
    }


}


public class CreatureModifier
{
    protected Creature creature;
    protected CreatureModifier next; //Linked list, refrence for next

    public CreatureModifier(Creature creature)
    {
        this.creature = creature;
    }

    public void Add(CreatureModifier cm)
    {
        if (next != null) next.Add(cm);
        else next = cm;
    }

    public virtual void Handle() => next?.Handle();

}



    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            //This will basically call next
            base.Handle();
        }
    }


public class IncreaseDefenseModifier : CreatureModifier
{
    public IncreaseDefenseModifier(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        Console.WriteLine($"Increasing {creature.Name}'s defense");
        creature.Defense += 5;
        //This will basically call next
        base.Handle();
    }
}

public class NoBonusesModifer : CreatureModifier
{
    public NoBonusesModifer(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        //Having no implementation breaks chain of responsability.
        //
    }
}
