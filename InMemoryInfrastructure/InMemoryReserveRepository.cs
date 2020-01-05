using System;
using System.Collections.Generic;
using modeling_mtg_room.Domain.Reserve;
using System.Linq;
using System.Threading.Tasks;

namespace InMemoryInfrastructure
{
    public class InMemoryReserveRepository : IReserveRepository
    {
        private readonly Dictionary<ReserveId, Reserve> data = new Dictionary<ReserveId, Reserve>();
        public Reserve Find(ReserveId id)
        {
            if(data.TryGetValue(id, out var target))
            {
                return target;
            }
            else
            {
                return null;
            }
        }

        public Task<Reserve> FindAsync(ReserveId id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Reserve> FindOfRoom(MeetingRooms room)
        {
            return data.Values.Where(x => x.Room.Equals(room));
        }

        public void Save(Reserve reserve)
        {
            data[reserve.Id] = reserve; 
        }

        public Task SaveAsync(Reserve reserve)
        {
            throw new NotImplementedException();
        }
    }
}
