using System;

namespace modeling_mtg_room.Domain.Reserves
{
    public class ReserverId 
    {
        public string Value {get;}

        public ReserverId (string value)
        {
            //todo: ルールなし

            this.Value = value;
        }
    }
}