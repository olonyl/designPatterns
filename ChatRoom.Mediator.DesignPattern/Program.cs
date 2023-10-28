﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatRoom.Mediator.DesignPattern
{
    public class Person {
        public string Name;
        public ChatRoom Room;
        private List<string> chatLog = new List<string>();

        public Person(string name) { 
            this.Name = name;
        }

        public void Say(string message)
        {
            Room.BroadCast(this.Name, message);
        }

        public void PrivateMessage(string who, string message)
        {
            Room.Message(Name, who, message);
        }

        public void Receive(string sender, string message)
        {
            string s = $"{sender}: '{message}'";
            chatLog.Add(s);
            Console.WriteLine($"[{Name}'s chat session] {s}");
        }
    }

    public class ChatRoom
    {
        private List<Person> people = new List<Person>();

        public void Join(Person p)
        {
            string joinMessage = $"{p.Name} joins the chat";
            BroadCast("source", joinMessage);

            p.Room = this;
            people.Add(p);
        }

        public void BroadCast(string source, string message)
        {
            foreach (var p in people)
            {
                if (p.Name != source)
                    p.Receive(source, message);
            }
        }
        public void Message(string source, string destination, string message)
        {
            people.FirstOrDefault(p => p.Name == destination)?.Receive(source, message);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var room = new ChatRoom();
            var john = new Person("John");
            var jane = new Person("Jane");

            room.Join(john);
            room.Join(jane);

            john.Say("Hi");
            jane.Say("oh, hey john");

            var simon = new Person("Simon");
            room.Join(simon);   
            simon.Say("hi everyone!");

            jane.PrivateMessage("Jane","Hi Simon! glad you could join us");

            Console.WriteLine("Hello World!");
        }
    }
}
