
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using modeling_mtg_room.Domain.Reserves;

namespace modeling_mtg_room.Domain.Repository
{
    public interface IReserveRepository
    {
        Task SaveAsync(Reserve reserve);
        void Save(Reserve reserve);
        Reserve Find(ReserveId id);
        Task<Reserve> FindAsync(ReserveId id);

        IEnumerable<Reserve> FindOfRoom(MeetingRooms room);

        Task DeleteAsync(ReserveId id);
    }
}