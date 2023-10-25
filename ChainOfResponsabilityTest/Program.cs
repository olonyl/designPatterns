using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainOfResponsabilityTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var goblin = new Goblin(game);
            game.Creatures.Add(goblin);

            game.Creatures.Add(new Goblin(game));

            game.Creatures.Add(new Goblin(game));

            var king = new GoblinKing(game);
            game.Creatures.Add(king);

            Console.WriteLine(goblin);
            Console.WriteLine(king);
        }
    }

    public abstract class Creature
    {
        private int attack, defense;
        public int Attack
        {
            get
            {
                var totalKings = game.Creatures.Count(w => w is GoblinKing);
                return totalKings + attack;

            }
            set => attack = value;
        }

        public int Defense {
            get
            {
                var totalGloblins = game.Creatures.Count();
                return totalGloblins;
            }
            set => defense = value; }

        private Game game;

        protected Creature(Game game)
        {
            this.game = game;
        }

        public override string ToString()
        {
            return $"Attack: {Attack}, Deffense : {Defense}";
        }
    }

    public class Goblin : Creature
    {

        public Goblin(Game game) : base(game)
        {
            this.Attack = 1;
            this.Defense = 1;
        }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game)
        {
            this.Attack = 3;
            this.Defense = 3;
        }
    }

    public class Game
    {
        public IList<Creature> Creatures = new List<Creature>();
    }

}
