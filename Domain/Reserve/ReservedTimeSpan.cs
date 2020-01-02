using System;
using System.Diagnostics.CodeAnalysis;

namespace modeling_mtg_room.Domain.Reserve
{
    /// <summary>
    /// 会議時間バリューオブジェクト
    /// </summary>
    public class ReservedTimeSpan : IEquatable<ReservedTimeSpan>
    {
        // DateTimeを受け取るインターフェース
        private IDateTime _dateTime;
        // 開始時間
        public ReservedTime _start;
        // 終了時間
        public ReservedTime _end;
        public ReservedTimeSpan(ReservedTime start,
                                ReservedTime end)
        {
            _start = start;
            _end = end;

            if(_start.Value > _end.Value)
                throw new ArgumentException("開始時間が終了時間を超えないようにして下さい");

        }
        /// <summary>
        /// 数値時間(e.g. 1.5, 0.25)を返す
        /// </summary>
        /// <value>e.g. 0.25, 0.5, 0.75, 1.0</value>
        public double TimeOfNumber
        {
            get 
            {
                double diff = (_end.Value - _start.Value).TotalMinutes / 60;
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

        public bool IsOverlap(ReservedTimeSpan other)
        {
            if (this.Equals(other)) return true;
            
            bool isOverlap = (this._start.Value >= other._start.Value && this._start.Value < other._end.Value)
                            || (this._start.Value <= other._start.Value && this._end.Value > other._start.Value);
                          //  || (this._start.Value <= other._start.Value && this._start.Value < other._end.Value);

            return isOverlap;

        }
    }
}