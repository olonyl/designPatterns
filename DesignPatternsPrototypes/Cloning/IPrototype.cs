namespace DesignPatternsPrototypes.Cloning
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }
}
