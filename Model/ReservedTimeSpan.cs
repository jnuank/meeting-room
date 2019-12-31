using System;
using System.Diagnostics.CodeAnalysis;

namespace modeling_mtg_room.Model
{
    /// <summary>
    /// 会議時間バリューオブジェクト
    /// </summary>
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

            if(_start.Minute % 15 != 0 || _end.Minute % 15 != 0)
                throw new ArgumentException("15分単位で入力して下さい");

            if(_start > _end)
                throw new ArgumentException("開始時間が終了時間を超えないようにして下さい");

            if((_dateTime.Now.AddDays(30)).Date < _start.Date)
                throw new ArgumentException("予約は30日後以内にして下さい");
            
            if(_start.Hour < 10 || _start.Hour > 19 || 
                _end.Hour < 10 || _end.Hour > 19)
                throw new ArgumentException("予約は10時から19時までにして下さい");
        }
        /// <summary>
        /// 数値時間(e.g. 1.5, 0.25)を返す
        /// </summary>
        /// <value>e.g. 0.25, 0.5, 0.75, 1.0</value>
        public double TimeOfNumber
        {
            get 
            {
                double diff = (_end - _start).TotalMinutes / 60;
                return TruncateSecondDecimalNumber(diff);
            }
        }
        /// <summary>
        /// 小数点第二位で切り捨て
        /// </summary>
        /// <param name="number">e.g. 1.500000</param>
        /// <returns>e.g. 1.50</returns>
        private double TruncateSecondDecimalNumber(double number)
        {
            // Truncate()は小数点第二位を指定での切り捨てができないので
            // 桁上げしている
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