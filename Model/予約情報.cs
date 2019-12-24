
namespace modeling_mtg_room.Model
{
    public class 予約情報
    {
        private long start;
        private long end;
        private 時間 じかん;
        public 予約情報(long start, long end)
        {
            this.start = start;
            this.end = end;
        }

        public long 時間
        {
            get {return (long)2.0;} 
        }

    }

}
