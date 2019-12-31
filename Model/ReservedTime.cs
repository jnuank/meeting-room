using System;
using System.Diagnostics.CodeAnalysis;

namespace modeling_mtg_room.Model
{
    public class ReservedTime : IEquatable<ReservedTime>
    {
        public DateTime Value { get; }
        private readonly IDateTime _dateTime;

        public ReservedTime(int year,
                            int month,
                            int day,
                            int hour,
                            int minute,
                            IDateTime dateTime = null)
        {
            _dateTime = dateTime ?? new ServerDateTime();

            if(minute % 15 != 0 )
                throw new ArgumentException("15分単位で入力して下さい");

            var value = new DateTime(year, month, day, hour, minute, 0);

            if((_dateTime.Now.AddDays(30)).Date < value.Date)
                throw new ArgumentException("予約は30日後以内にして下さい");
            
            // 秒は関係無いので0秒で統一
            this.Value = value;

        }

        public bool Equals(ReservedTime other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return this.Value == other.Value;
        }
    }
}