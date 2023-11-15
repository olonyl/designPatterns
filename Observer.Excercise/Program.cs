using Coding.Exercise;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Observer.Excercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();

            var rat1 = new Rat(game);
            var rat2 = new Rat(game);
            var rat3 = new Rat(game);
            var rat4 = new Rat(game);

            rat1.Dispose();

        }
    }
}

namespace Coding.Exercise
{
    public class Game
    {
        // todo
        // remember - no fields or properties!
        public event EventHandler RatEntersHandler, RatLeavesHandler;
        public event EventHandler<Rat> NotifyRatHandler;

        public void FireRatLeavesHandler(object sender)
        {
            RatLeavesHandler?.Invoke(sender, EventArgs.Empty);
        }

        public void FireRatEntersHandler (object sender)
        {
            RatEntersHandler?.Invoke(sender, EventArgs.Empty);
        }

        public void FireNotifyRat(object sender, Rat rat)
        {
            NotifyRatHandler?.Invoke(sender, rat);
        }
    }

    public class Rat : IDisposable
    {
        public int Attack = 1;
        private Game game;
        public Rat(Game game)
        {
            this.game = game;

            this.game.RatLeavesHandler += (sender, args) =>  Attack--;

            this.game.RatEntersHandler += (sender, args) =>
            {
                if (sender != this)
                {
                    Attack++;
                    this.game.FireNotifyRat(this, (Rat)sender);
                }   
            };
            this.game.NotifyRatHandler += (sender, rat) =>
            {
                if (rat == this)
                    ++Attack;
            };

            this.game.FireRatEntersHandler(this);
        }


        public void Dispose()
        {
            game.FireRatLeavesHandler(this); 
        }
    }
}

