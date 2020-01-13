using System;
using modeling_mtg_room.Domain.Reserves;

namespace Domain.Usages
{
    /// <summary>
    /// 利用実績
    /// </summary>
    public class Usage 
    {
        public readonly DateTime startDateTime;
        public readonly DateTime endDateTime;
        public readonly modeling_mtg_room.Domain.Reserves.MeetingRooms room;
        
    }
}