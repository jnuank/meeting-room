using Domain.Usages;
using Domain.MeetingRooms;
using System;

namespace Domain.Usages
{
    public interface IUsageRepository
    {
        void Save(Usage usage);

        Usage Find(MeetingRoom room, DateTime start, DateTime end);
        
    }
}