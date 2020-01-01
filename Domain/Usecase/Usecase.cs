using System;

namespace modeling_mtg_room.Domain.Reserve
{
    public class ReserveApplication
    {
        private readonly IDateTime dateTime;
        
        public ReserveApplication(IDateTime dateTime = null)
        {
            // デフォルトではサーバが保持する時間を使用する
            this.dateTime = dateTime ?? new ServerDateTime();
        }
        public Reserve ReserveMeetingRoom(string room,
                                            int startYear, int startMonth, int startDay, int startHour, int startMinute,
                                            int endYear, int endMonth, int endDay, int endHour, int endMinute,
                                            int reserverOfNumber,
                                            string reserverId)
        {
            MeetingRooms mtgRoom;
            if(!Enum.TryParse(room, true, out mtgRoom))
                throw new ApplicationException("指定された会議室が存在しません");

            var startTime = new ReservedTime(startYear, startMonth, startDay, startHour, startMinute, this.dateTime);
            var endTime = new ReservedTime(endYear, endMonth, endDay, endHour, endMinute, this.dateTime);
            var timeSpan = new ReservedTimeSpan(startTime, endTime);
            var reserver = new ReserverOfNumber(reserverOfNumber);
            var id = new ReserverId(reserverId);
            
            return new Reserve(mtgRoom, timeSpan, reserver, id);
        }
            //todo: 入れた時間が、バッティングしていないかどうかをチェックする必要がある
    }
}