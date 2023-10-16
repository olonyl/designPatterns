using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProxyDesignPattern.ViewModel
{
    public class PersonViewModel
        : INotifyPropertyChanged
    {
        private readonly Person person;

        public string FirstName
        {
            get => person.FirstName;
            set
            {
                if (person.FirstName == value) return;
                person.FirstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string LastName
        {
            get => person.LastName;
            set
            {
                if (person.LastName == value) return;
                person.LastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string FullName
        {
            get => $"{FirstName} {LastName}".Trim();
            set
            {
                if (value == null)
                {
                    FirstName = LastName = null;
                    return;
                }
                var items = value.Split();
                if (items.Length > 0)
                    FirstName = items[0];
                if (items.Length > 1)
                    LastName = items[1];
            }
        }

        public PersonViewModel(Person person)
        {
            this.person = person;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

}

