using System;
using Xunit;
using Domain.Reserves;
using Moq;
using System.Collections.Generic;
using Domain.MeetingRooms;

namespace Test
{
    public class MeetingRoomsTest
    {
        [Fact]
        public void 会議室Aのステータスを入室にする()
        {
            MeetingRoom.A.EnteringRoom();

            Assert.Equal(RoomStatus.USE, MeetingRoom.A.Status);
        }
    }
}