using System;
using Domain.Reserves;

namespace Domain.Usages
{
    /// <summary>
    /// 利用実績
    /// </summary>
    public class Usage 
    {
        public readonly DateTime startDateTime;
        public readonly DateTime endDateTime;
        public readonly Domain.Reserves.MeetingRooms room;

    }
}