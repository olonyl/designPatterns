using System.Collections.Generic;

namespace DesignPatternsComposite
{
    public static class ExtensionMethods3
    {
        public static void ConnectTo(this IEnumerable<Neuron> self
            , IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;
            foreach (Neuron from in self)
                foreach (Neuron to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
        }
    }
}
