namespace modeling_mtg_room.Domain.Reserve
{
    public class Reserve
    {
        public MeetingRooms Room { get; }
        public ReservedTimeSpan TimeSpan { get; }
        public ReserverOfNumber ReserverOfNumber { get; }
        public ReserverId ReserverId { get; }
        public Reserve (MeetingRooms room,
                    ReservedTimeSpan timeSpan,
                    ReserverOfNumber reserverOfNumber,
                    ReserverId reserverId)
        {
            this.Room = room;
            this.TimeSpan = timeSpan;
            this.ReserverOfNumber = reserverOfNumber;
            this.ReserverId = reserverId;
        }
    }
}