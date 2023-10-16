namespace ProxyDesignPattern.CompositeProxyArrayBackend
{
    public class MasonrySettgings
    {
        public bool? All
        {
            set
            {
                if (!value.HasValue) return;
                Pillars = value.Value;
                Walls = value.Value;
                Floors = value.Value;
            }

            get
            {
                if (Pillars == Walls && Walls == Floors) return Pillars;
                return null;
            }
        }
        public bool Pillars, Walls, Floors;

    }
}