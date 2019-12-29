using System;

namespace modeling_mtg_room.Model
{
    public class ServerDateTime : IDateTime

    {
        public DateTime Now => DateTime.Now;
    }
}