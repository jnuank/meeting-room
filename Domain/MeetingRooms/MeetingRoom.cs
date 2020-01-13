
using System;

namespace Domain.MeetingRooms
{
    public class MeetingRoom
    {
        // idは識別子となるため、一度初期化したら二度と変更させない
        public string RoomName { get; }

        public RoomStatus Status { get; private set; }

        public MeetingRoom(string roomName, RoomStatus status)
        {
            if(string.IsNullOrEmpty(roomName))
                throw new ArgumentException("Roomの値が空です");

            this.RoomName = roomName;
            this.Status = status;
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
        
    }
}