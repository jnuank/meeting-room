
using System;

namespace modeling_mtg_room.Model
{
    public class 予約情報
    {
        // 開始時間
        private DateTime start;
        // 終了時間
        private DateTime end;
        // 予約時間
        private 時間 じかん;
        public 予約情報(DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;
        }

        public long 時間
        {
            get {return (long)((end - start).TotalMinutes / 60);}
        }

    }

}
