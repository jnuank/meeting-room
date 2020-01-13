
using System;

namespace Domain.MeetingRooms
{
    public class MeetingRoom
    {
        // idは識別子となるため、一度初期化したら二度と変更させない
        private readonly string id;
        public string RoomName { get; }

        public RoomStatus Status { get; private set; }

        public MeetingRoom(string id, string roomName, RoomStatus status)
        {
            if(!string.IsNullOrEmpty(id))
                throw new ArgumentException("idの値が空です");
            
            this.id = id;
            this.RoomName = roomName;
            this.Status = status;
        }
        
        public string Id
        {
            get { return id; }
        }
        
        public bool EnteringRoom()
        {
            this.Status = RoomStatus.USE;
            return true;
        }

        public bool LeavingRoom()
        {
            this.Status = RoomStatus.VACANCY;
            return true;
        }
        
        // 会議室に持たせたい振る舞いとは何。
    }
}