using System;
using Domain.MeetingRooms;
using Domain.Usages;

namespace InMemoryInfrastructure
{
    public class InMemoryUsageRepository : IUsageRepository
    {
        public Usage Find(MeetingRoom room, DateTime start, DateTime end)
        {

            // 仮実装
            return new Usage(start, end, room);
        }

        public void Save(Usage usage)
        {
            throw new NotImplementedException();
        }
    }
}