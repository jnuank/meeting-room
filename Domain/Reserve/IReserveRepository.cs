
using System;
using System.Collections.Generic;
using modeling_mtg_room.Domain.Reserve;

namespace modeling_mtg_room.Domain.Reserve
{
    public interface IReserveRepository
    {
        void Save(Reserve reserve);
        Reserve Find(ReserveId id);

        IEnumerable<Reserve> FindOfRoom(MeetingRooms room);
    }
}