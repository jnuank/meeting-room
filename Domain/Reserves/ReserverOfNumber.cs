using System;

namespace modeling_mtg_room.Domain.Reserves
{
    public class ReserverOfNumber
    {
        public int Value {get;}

        public ReserverOfNumber (int value)
        {
            // Todo: 仮ルールで1人〜100人まで
            if(value < 0 || value > 100)
                throw new ArgumentException();

            this.Value = value;
        }
        public string ValueString
        {
            get { return this.Value.ToString(); }
        }
    }
}