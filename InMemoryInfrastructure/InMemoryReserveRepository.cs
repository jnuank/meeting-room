using System;
using System.Collections.Generic;
using modeling_mtg_room.Domain.Reserve;

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

        public void Save(Reserve reserve)
        {
            data[reserve.Id] = reserve; 
        }
    }
}
