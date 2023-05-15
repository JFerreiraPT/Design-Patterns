Foo x = await Foo.CreateAsync(); 


public class Foo
{
     //We cant use await inside a constructor, so factories can help in that cases
    private Foo()
    {

    }

    private async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }

    public static Task<Foo> CreateAsync()
    {
        var result = new Foo();
        return result.InitAsync();
    }
}

