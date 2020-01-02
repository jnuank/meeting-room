using System;
using System.Diagnostics.CodeAnalysis;

namespace modeling_mtg_room.Domain.Reserve
{
    public class ReservedTime : IEquatable<ReservedTime>
    {
        public static readonly int MINUTES_PER_TIME_BLOCK = 15; 
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

            if(minute % MINUTES_PER_TIME_BLOCK != 0 )
                throw new ArgumentException($"{MINUTES_PER_TIME_BLOCK}分単位で入力して下さい");

            var value = new DateTime(year, month, day, hour, minute, 0);

            if((_dateTime.Now.AddDays(30)).Date < value.Date)
                throw new ArgumentException("予約は30日後以内にして下さい");

            if(value.Hour < 10 || value.Hour > 19)
                throw new ArgumentException("予約は10時から19時までにして下さい");
            
            // 秒は関係無いので0秒で統一
            this.Value = value;

        }

        public ReservedTime AddTimeBlock(int timeBlock)
        {
            DateTime time = this.Value.AddMinutes(timeBlock * MINUTES_PER_TIME_BLOCK);
            return new ReservedTime(time.Year,
                                    time.Month,
                                    time.Day,
                                    time.Hour,
                                    time.Minute);
        }

        public bool Equals(ReservedTime other)
        {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return this.Value == other.Value;
        }
    }
}