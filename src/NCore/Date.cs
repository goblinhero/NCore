using System;
using NCore.Extensions;

namespace NCore
{
    public class Date : IValueType, IComparable<Date>
    {
        protected Date()
        {
        }

        public Date(int daysSince1970)
        {
            DateDays = daysSince1970;
        }

        public int DateDays { get; private set; }
        public DateTime ToDateTime => DateDays.ToDate();

        public static Date Today => new Date(DateTime.Today.DaysSince1970_01_01());

        public int CompareTo(Date other)
        {
            return DateDays.CompareTo(other.DateDays);
        }

        protected bool Equals(Date other)
        {
            return DateDays == other.DateDays;
        }

        public static implicit operator int(Date d)
        {
            return d?.DateDays ?? 0;
        }

        public static implicit operator Date(int d)
        {
            return new Date(d);
        }

        public static implicit operator Date(int? d)
        {
            return d.HasValue ? new Date(d.Value) : null;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Date) obj);
        }

        public override int GetHashCode()
        {
            return DateDays;
        }

        public static bool operator ==(Date left, Date right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Date left, Date right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return ToDateTime.ToString("d");
        }
    }
}