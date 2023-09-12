using Autofac;
using DesignPatternsSingletone.BasicImplementation;
using DesignPatternsSingletone.DependencyInjection;
using NUnit.Framework;

namespace DesignPatternsSingletoneTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsSignletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;

            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }
        [Test]
        public void SingletonTotalPopulation()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] { "Seoul", "Mexico City"};
            int tp = rf.GetTotalPopulation(names);

            Assert.That(tp, Is.EqualTo(17500000+ 17400000));
        }
        [Test]
        public void ConfigurablePopulationTest()
        {
            var rf = new ConfigurableRecordFinder(new DummyDatabase());
            var names = new[] { "alpha", "gamma" };
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(4));

        }
        [Test]
        public void DIPopulationTest()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<OrdinaryDatabase>()
                .As<IDatabase>()
                .SingleInstance();
            cb.RegisterType<ConfigurableRecordFinder>();
            using(var c = cb.Build())
            {
                var rf = c.Resolve<ConfigurableRecordFinder>();
                
            }
        }
    }
}