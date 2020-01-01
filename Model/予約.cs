namespace modeling_mtg_room.Model
{
    public class 予約
    {
        public MeetingRooms Room { get; }
        public ReservedTimeSpan TimeSpan { get; }
        public 予約人数 ReserverOfNumber { get; }
        public 予約者ID ReserverId { get; }
        public 予約 (MeetingRooms room,
                    ReservedTimeSpan timeSpan,
                    予約人数 reserverOfNumber,
                    予約者ID reserverId)
        {
            this.Room = room;
            this.TimeSpan = timeSpan;
            this.ReserverOfNumber = reserverOfNumber;
            this.ReserverId = reserverId;
        }
    }
}