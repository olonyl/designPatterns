using Coding.Exercise;
using Stateless;
using System;
using System.Linq;

namespace StateMachineWithStateless
{
    public enum Health
    {
        NonReproductive, 
        Pregnant, 
        Reproductive
    }

    public enum Activity
    {
        GiveBirth,
        ReachPuberty, 
        HaveAbortion,
        HaveUnprotectedSex,
        Historectomy
    }


  
    internal class Program
    {
      
public static bool ParentsNotWatching { get; private set; }

        static void Main(string[] args)
        {
            //var machine = new StateMachine<Health, Activity>(Health.NonReproductive);
            // machine.Configure(Health.NonReproductive)
            //     .Permit(Activity.ReachPuberty, Health.Reproductive);

            // machine.Configure(Health.Reproductive)
            //     .Permit(Activity.Historectomy, Health.NonReproductive)
            //     .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant, () => ParentsNotWatching);

            // machine.Configure(Health.Pregnant)
            //     .Permit(Activity.GiveBirth, Health.Reproductive)
            //     .Permit(Activity.HaveAbortion, Health.Reproductive);


            var cl = new CombinationLock(new[] { 1, 2, 3, 4, 5 });
            Console.WriteLine(cl.Status);


            cl.EnterDigit(1);
            Console.WriteLine(cl.Status);

            cl.EnterDigit(2);
            Console.WriteLine(cl.Status);

            cl.EnterDigit(3);
            Console.WriteLine(cl.Status);

            cl.EnterDigit(4);
            Console.WriteLine(cl.Status);

            cl.EnterDigit(5);
            Console.WriteLine(cl.Status);

        }
    }
}

namespace Coding.Exercise
{
    public class CombinationLock
    {
        private readonly int[] combination;
        private string SecurityKey
        {
            get
            {
                return string.Join("", combination.Select(s => s.ToString()).ToArray());
            }
        }

        public CombinationLock(int[] combination)
        {
            // todo
            this.combination = combination;
        }

        // you need to be changing this on user input
        public string Status = "LOCKED";
        public bool isFirstDigit = true;
        
        public void EnterDigit(int digit)
        {
            var strDigit = digit.ToString();

            if(isFirstDigit)
                Status = string.Empty;

            Status += strDigit;

            if (Status.Length == SecurityKey.Length && Status == SecurityKey)
                Status = "OPEN";
            else if (!SecurityKey.Contains(Status))
                Status = "ERROR";
             

            isFirstDigit= false;
        }
    }
}
