using System;
using System.Collections.Generic;

namespace HandmadeStateMachine
{
    public enum State
    {
        OffHook,
        Connecting, 
        Connected, 
        OnHold
    }

    public enum Trigger
    {
        CallDialed,
        Hungup,
        CallConnected,
        PlaceOnHold,
        TakenOffHold,
        LeftMessage
    }
    internal class Program
    {
        private static Dictionary<State, List<(Trigger, State)>> rules
            = new Dictionary<State, List<(Trigger, State)>>
            {
                [State.OffHook] = new List<(Trigger, State)>
                {
                    (Trigger.CallDialed, State.Connecting)
                },
                [State.Connecting] = new List<(Trigger, State)>
                {
                    (Trigger.Hungup,State.OffHook),
                    (Trigger.CallConnected, State.Connected)
                },
                [State.Connected] = new List<(Trigger, State)>
                {
                    (Trigger.LeftMessage, State.OffHook),
                    (Trigger.Hungup, State.OffHook),
                    (Trigger.PlaceOnHold, State.OnHold)
                },
                [State.OnHold] = new List<(Trigger, State)>
                {
                    (Trigger.TakenOffHold, State.Connected),
                    (Trigger.Hungup, State.OffHook)
                }
            };
        static void Main(string[] args)
        {
            var state = State.OffHook;

            while (true)
            {
                Console.WriteLine($"The phone is currently {state}");
                Console.WriteLine("Select a trigger: ");
                for (int i = 0; i < rules[state].Count; i++)
                {
                   var (t, _) = rules[state][i];
                   Console.Write($"{i}. {t}");
                }

                int input = int.Parse(Console.ReadLine());

                var (_, s) = rules[state][input];
                state = s;
            }
        }
    }
}
