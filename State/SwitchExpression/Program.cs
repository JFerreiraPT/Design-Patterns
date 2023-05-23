using System;

namespace DotNetDesignPatternDemos.Behavioral.State.SwitchExpressions
{
    enum Chest
    {
        Open,
        Closed,
        Locked
    }

    enum Action
    {
        Open,
        Close
    }

    public class SwitchExpressions
    {
        static Chest Manipulate(Chest chest,
          Action action, bool haveKey) =>
          (chest, action, haveKey) switch
          {
              //We use _ because its irrelevant if we have the kwy or not int this situation
              (Chest.Closed, Action.Open, _) => Chest.Open,
              //If the chest is locked, we want to open it and we have a key => the chest will open
              (Chest.Locked, Action.Open, true) => Chest.Open,
              (Chest.Open, Action.Close, true) => Chest.Locked,
              (Chest.Open, Action.Close, false) => Chest.Closed,
              //Default case => the current state is returned, nothing happens
              _ => chest
          };

        static Chest Manipulate2(Chest chest,
          Action action, bool haveKey)
        {
            switch (chest, action, haveKey)
            {
                case (Chest.Closed, Action.Open, _):
                    return Chest.Open;
                case (Chest.Locked, Action.Open, true):
                    return Chest.Open;
                case (Chest.Open, Action.Close, true):
                    return Chest.Locked;
                case (Chest.Open, Action.Close, false):
                    return Chest.Closed;
                default:
                    Console.WriteLine("Chest unchanged");
                    return chest;
            }
        }

        public static void Main(string[] args)
        {
            Chest chest = Chest.Locked;
            Console.WriteLine($"Chest is {chest}");

            // unlock with key
            chest = Manipulate(chest, Action.Open, true);
            Console.WriteLine($"Chest is now {chest}");

            // close it!
            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is now {chest}");

            // close it again!
            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is now {chest}");
        }
    }
}