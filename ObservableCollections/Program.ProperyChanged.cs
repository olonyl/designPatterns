using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace ObserverViaSpecialInterface
{
   public class Market : INotifyPropertyChanged
    {
        private float volatility;
        public float Volatitility
        {
            get => volatility;
            set  {
                if(value.Equals(volatility)) return;
                volatility = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Program
    {
        static void AnotherMain(string[] args)
        {
            var market = new Market();
            market.PropertyChanged += (sender, eventArgs) =>
            {
                if (eventArgs.PropertyName == "Volatility")
                {

                }
            };
        }
    }
}
