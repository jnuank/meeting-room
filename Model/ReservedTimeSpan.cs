using System;
using System.Diagnostics.CodeAnalysis;

namespace modeling_mtg_room.Model
{
    // 予約時間
    public class ReservedTimeSpan : IEquatable<ReservedTimeSpan>
    {
        // DateTimeを受け取るインターフェース
        private IDateTime _dateTime;
        // 開始時間
        private DateTime _start;
        // 終了時間
        private DateTime _end;
        public ReservedTimeSpan(DateTime start,
                                DateTime end,
                                IDateTime dateTime = null)
        {
            // 日付がDIされていたら、変数に入れる
            _dateTime = dateTime ?? new ServerDateTime(); 

            _start = start;
            _end = end;
            // 15分単位でないとエラー
            if(_start.Minute % 15 != 0 || _end.Minute % 15 != 0)
                throw new ArgumentOutOfRangeException();
            // _start<_endとなっていること
            if(_start > _end)
                throw new ArgumentOutOfRangeException();

            if((_dateTime.Now.AddDays(30)).Date < _start.Date)
                throw new ArgumentOutOfRangeException();
        }
        public double TimeOfNumber
        {
            get 
            {
                double diff = (_end - _start).TotalMinutes / 60;
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
            return this._start == other._start && this._end == other._end;
        }
    }
}