using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace PropertyDependencies
{
    public class PropertyNotificationSupport : INotifyPropertyChanged
    {
        private readonly Dictionary<string, HashSet<string>> affectedBy
            = new Dictionary<string, HashSet<string>>();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            foreach(var affected in affectedBy)
                if (affected.Value.Contains(propertyName))
                    OnPropertyChanged(affected.Key);
        }

        protected Func<T> property<T>(string name, Expression<Func<T>> expr)
        {
            Console.WriteLine($"Creating computed property for expression {expr}");

            var visitor = new MemberAccessVisitor(GetType());
            visitor.Visit(expr);

            if (visitor.PropertyNames.Any())
            {
                if(!affectedBy.ContainsKey(name))
                    affectedBy.Add(name, new HashSet<string>());
                foreach(var propName in visitor.PropertyNames)
                    if(propName != name)
                        affectedBy[name].Add(propName);
            }
            return expr.Compile();
        }

        private class MemberAccessVisitor : ExpressionVisitor
        {
            private readonly Type declaringType;
            public readonly IList<string> PropertyNames = new List<string>();

            public MemberAccessVisitor(Type declaringType)
            {
                this.declaringType = declaringType;
            }

            public override Expression Visit(Expression expr)
            {
                if(expr != null && expr.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExpr = (MemberExpression)expr;
                    if(memberExpr.Member.DeclaringType == declaringType)
                        PropertyNames.Add(memberExpr.Member.Name);
                }

                return base.Visit(expr);
            }
        }
    }
    public class Person : PropertyNotificationSupport
    {
        private int age;
        private bool citizen;

        public bool Citizen
        {
            get => citizen;
            set
            {
                if (citizen == value) return;

                citizen = value;
                OnPropertyChanged();
            }
        }
        public int Age
        {
            get => age;
            set
            {
                if (age == value) return;
                age = value;
                OnPropertyChanged();
            }
        }
        private readonly Func<bool> canVote;
        public bool CanVote => canVote();

        public Person()
        {
            canVote = property(nameof(CanVote), () => Age >= 16 && Citizen);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var p = new Person();
            p.PropertyChanged += (sender, eventArgs) =>
            {
                Console.WriteLine($"{eventArgs.PropertyName} changed");
            };

            p.Age = 15;
            p.Citizen = true;   

        }
    }
}
