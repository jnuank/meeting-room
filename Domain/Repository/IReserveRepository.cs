using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Reserves;

namespace Domain.Repository
{
    public interface IReserveRepository 
    {
        Task SaveAsync(Reserve reserve);
        void Save(Reserve reserve);
        Reserve Find(ReserveId id);
        Task<Reserve> FindAsync(ReserveId id);
        IEnumerable<Reserve> FindOfRoom(Domain.Reserves.MeetingRooms room);
        Task DeleteAsync(ReserveId id);
        Task<bool> ExistsAsync(ReserveId id);
    }
}