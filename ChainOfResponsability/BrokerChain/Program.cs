//The problem with previoous implementation is that outside modifiers classes mutate Creature!

//Brocker Chain -> With mediator

var game = new Game();
var goblin = new Creature(game, "String Goblin", 3, 3);

Console.WriteLine(goblin);

//We implemented disposal so we can use using
using( new DoubleAttackModifier(game, goblin))
{
    Console.WriteLine(goblin);
}

//here the efects are disposed, so we will return to base status

public class Game
{
    public event EventHandler<Query> Queries;

    public void PerformQuery(object sender, Query q)
    {

        Queries?.Invoke(sender, q);
    }
}


public class Creature
{
    private Game game;
    public string Name;
    private int attack, defense;

    public int Attack
    {
        //This way we can change attack without changing Creature base status
        get
        {
            var q = new Query(Name, Query.Argument.Attack, attack);
            game.PerformQuery(this, q);
            return q.Value;
        }
    }

    public int Defense
    {
        //This way we can change attack without changing Creature base status
        get
        {
            var q = new Query(Name, Query.Argument.Defense, defense);
            game.PerformQuery(this, q);
            return q.Value;
        }
    }


    public Creature(Game game, string name, int attack, int defense)
    {
        this.game = game;
        Name = name;
        this.attack = attack;
        this.defense = defense;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Attack: {Attack}, Defense: {Defense}";
    }


}

public class Query
{
    public string CreatureName;
    public enum Argument
    {
        Attack, Defense
    }

    public Argument WhatToQuery;
    public int Value;

    public Query(string creatureName, Argument whatToQuery, int value)
    {
        CreatureName = creatureName;
        WhatToQuery = whatToQuery;
        Value = value;
    }
}

public abstract class CreatureModifier : IDisposable
{
    protected Game game;
    protected Creature creature;

    protected CreatureModifier(Game game, Creature creature)
    {
        this.game = game;
        this.creature = creature;
        //Subscribe 
        game.Queries += Handle;
    }

    protected abstract void Handle(object sender, Query q);

    public void Dispose()
    {
        game.Queries -= Handle;
    }
}

//We apply modifiers without changing the creature itself
public class DoubleAttackModifier : CreatureModifier
{
    public DoubleAttackModifier(Game game, Creature creature) : base(game, creature)
    {
    }

    protected override void Handle(object sender, Query q)
    {
        if (q.CreatureName == creature.Name && q.WhatToQuery == Query.Argument.Attack)
        {
            q.Value *= 2;
        }
    }
}

public class IncreaseDefenseModifier : CreatureModifier
{
    public IncreaseDefenseModifier(Game game, Creature creature) : base(game, creature)
    {
    }

    protected override void Handle(object sender, Query q)
    {
        if (q.CreatureName == creature.Name && q.WhatToQuery == Query.Argument.Defense)
        {
            q.Value += 3;
        }
    }
}
