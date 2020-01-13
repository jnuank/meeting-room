using System;
namespace Domain.Reserves
{
    public class ReserveId : IEquatable<ReserveId>
    {
        public string Value { get; }

        public ReserveId(string value)
        {
            // todo: ルールいまのところ無し
            this.Value = value;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ReserveId) obj);
        }

        public bool Equals(ReserveId other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return string.Equals(Value, other.Value);
        }

        public override int GetHashCode() {
            return (this.Value != null ? this.Value.GetHashCode() : 0);
        }
    }
}