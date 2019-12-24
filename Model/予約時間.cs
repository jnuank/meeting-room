using System;
using System.Diagnostics.CodeAnalysis;

namespace modeling_mtg_room.Model
{
    public class 予約時間 : IEquatable<予約時間>
    {
        public long Value { get; }
        public 予約時間(long time)
        {
            Value = time;
        }

        public bool Equals(予約時間 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Value == other.Value;
        }
    }

}