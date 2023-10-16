namespace ProxyDesignPattern.PropertyProxy
{
    public class Creature
    {
        private Property<int> agility = new Property<int>();

        public int Agility
        {
            get => agility;
            set => agility.Value = value;
        }
    }
}