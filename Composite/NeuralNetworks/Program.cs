


using System.Collections;
using System.Collections.ObjectModel;

var neuron1 = new Neuron();
var neuron2 = new Neuron();

neuron1.ConnectTo(neuron2);

var layer1 = new NeuronLayer();
var layer2 = new NeuronLayer();
layer1.ConnectTo(layer2);

neuron1.ConnectTo(layer1);

public static class ExtensionMethods
{
    public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
    {
        //iterate every neurons in self and any neuros on other and connect one to each other
        if (ReferenceEquals(self, other)) return;
        foreach (var from in self)
        foreach (var to in other)
        {
                from.Out.Add(to);
                to.In.Add(from);

        }
    }
}

public class Neuron : IEnumerable<Neuron>
{
    public float Value;
    //We can a have connections coming in and connections comming out
    public List<Neuron> In, Out;



    public IEnumerator<Neuron> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public class NeuronLayer : Collection<Neuron>
{

}