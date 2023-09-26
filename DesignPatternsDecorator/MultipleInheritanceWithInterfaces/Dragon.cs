namespace DesignPatternsDecorator.MultipleInheritanceWithInterfaces
{
    public class Dragon : IBird, ILizard
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();
        private int weight;

        public int Weight
        {
            get => weight;
            set
            {
                weight = value;
                bird.Weight = weight;
                lizard.Weight = weight;
            }
        }

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }
    }
}
