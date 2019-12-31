using System;

namespace modeling_mtg_room.Model
{
    public class 予約人数
    {
        public int Value {get;}

        public 予約人数 (int value)
        {
            // Todo: 仮ルールで1人〜100人まで
            if(value < 0 || value > 100)
                throw new ArgumentException();

            this.Value = value;
        }
    }
}