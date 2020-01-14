
using System;
using System.Linq;
using Domain.Common;

namespace Domain.MeetingRooms
{
    public abstract class MeetingRoom : Enumeration
    {
        public static readonly MeetingRoom A = new RoomA();

        public string RoomName { get; }
        public RoomStatus Status { get; protected set; }

        protected MeetingRoom(int id, string name) : base(id, name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentException("Roomの値が空です");

            this.RoomName = name;
        }

        public static MeetingRoom GetMaterialType(int id)
        {
            var materialTypes = Enumeration.GetAll<MeetingRoom>().Cast<MeetingRoom>();

            return materialTypes.FirstOrDefault(x => x.Id == id);
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

        public MeetingRoom(string roomName, RoomStatus status)
        {
            if(string.IsNullOrEmpty(roomName))
                throw new ArgumentException("Roomの値が空です");

            this.RoomName = roomName;
            this.Status = status;
        }
        
        private class RoomA : MeetingRoom
        {
            public RoomA() : base(0, "A"){ }

        }
    }
}