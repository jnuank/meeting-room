using System;
using Domain.Usages;


namespace Domain.Application
{
    public class UsageApplication
    {
        private readonly IUsageRepository repository;

        public UsageApplication(IUsageRepository repository)
        {
            this.repository = repository;
        }

        public void 会議室の利用を開始する(string room)
        {
            Reserves.MeetingRooms mtgRoom;
            if(!Enum.TryParse(room, true, out mtgRoom))
                throw new ApplicationException("指定された会議室が存在しません");
            
            var rooms = new MeetingRooms.MeetingRoom(mtgRoom.ToString(), MeetingRooms.RoomStatus.VACANCY);

            rooms.EnteringRoom();
        }

        public bool 会議室が利用中かどうか確認する(string room)
        {
            Reserves.MeetingRooms mtgRoom;
            if(!Enum.TryParse(room, true, out mtgRoom))
                throw new ApplicationException("指定された会議室が存在しません");
            
            // TODO:仮実装
            return true;
        }

        public string 会議室の現在の利用状況を確認する()
        {
            throw new NotImplementedException();
        }
    }
}