var room = new ChatRoom();

var John = new Person("John");
var jane = new Person("Jane");

room.Join(John);
room.Join(jane);

John.Say("hi");
jane.Say("oh, hey jonh");

var simon = new Person("Simon");
room.Join(simon);
simon.PrivateMessage(jane.Name, "Hello Jane");


public class Person
{
    public string Name;
    public ChatRoom Room;
    //history of messages
    private List<string> chatLog = new List<string>();

    public Person(string name)
    {
        Name = name;
    }

    public void Say(string message)
    {
        Room.Broadcast(Name, message);
    }

    public void PrivateMessage(string who, string message)
    {
        Room.Message(Name, who, message);
    }

    public void Receive(string sender, string message)
    {
        string s = $"{sender}: '{message}'";
        chatLog.Add(s);
        Console.WriteLine($"[{Name}'s chat session] {s}");
    }
}

//this is a mediator!!
public class ChatRoom
{
    private List<Person> people = new List<Person>();

    public void Join(Person p)
    {
        string joinMsg = $"{p.Name} joins the chat";
        Broadcast("room", joinMsg);
        p.Room = this;
        people.Add(p);
    }

    public void Broadcast(string source, string msg)
    {
        foreach (var p in people)
        {
            if (p.Name != source)
                p.Receive(source, msg);
        }
    }
    public void Message(string source, string destination, string message)
    {
        people.FirstOrDefault(p => p.Name == destination)
            ?.Receive(source, message);
    }
}