using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DesignPatternsComposite
{
    public class Neuron : IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In, Out;

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }
        IEnumerator<Neuron> IEnumerable<Neuron>.GetEnumerator()
        {
            yield return this;
        }
    }
}
