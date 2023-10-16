using System;
using System.Diagnostics;

namespace ProxyDesignPattern.ValueProxy
{
    [DebuggerDisplay("{value*100.0f}%")]
    public struct Percentage : IEquatable<Percentage>
    {
        public bool Equals(Percentage other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            return obj is Percentage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(Percentage left, Percentage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Percentage left, Percentage right)
        {
            return !left.Equals(right);
        }

        private readonly float value;

        internal Percentage(float value)
        {
            this.value = value;
        }

        public static float operator *(float f, Percentage p)
        {
            return f * p.value;
        }
        public static Percentage operator +(Percentage p, Percentage f)
        {
            return new Percentage(f.value + p.value);
        }

        public override string ToString()
        {
            return $"{value * 100}%";
        }
    }
}