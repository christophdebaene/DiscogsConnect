using System;

namespace DiscogsConnect
{
    internal class Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
    {
        public static readonly Unit Value = new Unit();
        public int CompareTo(Unit other) => 0;
        int IComparable.CompareTo(object obj) => 0;
        public override int GetHashCode() => 0;
        public bool Equals(Unit other) => true;
        public override bool Equals(object obj) => obj is Unit;
        public static bool operator ==(Unit _1, Unit _2) => true;
        public static bool operator !=(Unit _1, Unit _2) => false;
        public override string ToString() => "()";
    }
}