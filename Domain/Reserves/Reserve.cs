using System;
namespace modeling_mtg_room.Domain.Reserves
{
    /// <summary>
    /// 予約エンティティ
    /// </summary>
    public class Reserve
    {
        public ReserveId Id { get; }
        public MeetingRooms Room { get; }
        public ReservedTimeSpan TimeSpan { get; }
        public ReserverOfNumber ReserverOfNumber { get; }
        public ReserverId ReserverId { get; }
        public Reserve (ReserveId id,
                    MeetingRooms room,
                    ReservedTimeSpan timeSpan,
                    ReserverOfNumber reserverOfNumber,
                    ReserverId reserverId)
        {
            this.Id = id;
            this.Room = room;
            this.TimeSpan = timeSpan;
            this.ReserverOfNumber = reserverOfNumber;
            this.ReserverId = reserverId;
        }
        public Reserve (MeetingRooms room,
                    ReservedTimeSpan timeSpan,
                    ReserverOfNumber reserverOfNumber,
                    ReserverId reserverId)
        {
            // todo: ここに、予約が埋まっていたら駄目ルール
            // todo: ただ、それをやるのは、リポジトリ層にアクセスしないといけない。
            // todo: ドメインサービスでやることかもしれない
            // todo: Duplicateみたいなやつ
            
            this.Id = new ReserveId(Guid.NewGuid().ToString("N"));
            this.Room = room;
            this.TimeSpan = timeSpan;
            this.ReserverOfNumber = reserverOfNumber;
            this.ReserverId = reserverId;
        }
    }
}