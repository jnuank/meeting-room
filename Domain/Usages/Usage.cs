using System;
using Domain.MeetingRooms;

namespace Domain.Usages
{
    /// <summary>
    /// 利用実績
    /// </summary>
    public class Usage 
    {
        public readonly DateTime startDateTime;
        public readonly DateTime endDateTime;
        public readonly MeetingRoom room;

        public Usage(DateTime startDateTime, DateTime endDateTime, MeetingRoom room)
        {
            this.endDateTime = endDateTime;
            this.startDateTime = startDateTime;
            this.room = room;
        }

    }
}