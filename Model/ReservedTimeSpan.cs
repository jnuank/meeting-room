using System;
using System.Diagnostics.CodeAnalysis;

namespace modeling_mtg_room.Model
{
    // 予約時間
    public class ReservedTimeSpan : IEquatable<ReservedTimeSpan>
    {
        // 開始時間
        private DateTime start;
        // 終了時間
        private DateTime end;
        public ReservedTimeSpan(DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;
        }
        public double 時間
        {
            get 
            {
                double diff = (end - start).TotalMinutes / 60;
                return TruncateSecondDecimalNumber(diff);
            }
        }
        // 小数点第二位で切り捨て
        private double TruncateSecondDecimalNumber(double number)
        {
            number *= 100;
            return Math.Truncate(number) / 100;
        }

        public bool Equals(ReservedTimeSpan other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.start == other.start && this.end == other.end;
        }
    }

}