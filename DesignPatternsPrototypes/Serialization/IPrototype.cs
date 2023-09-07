namespace DesignPatternsPrototypes.Serialization
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }
}
