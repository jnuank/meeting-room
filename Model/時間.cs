using System;

namespace modeling_mtg_room.Model
{
    public class 時間
    {
        public DateTime Value { get; }
        
        public 時間(DateTime time)
        {
            Value = time;
        }
    }

}