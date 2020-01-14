using System;
using Xunit;
using Domain.Reserves;
using Moq;
using System.Collections.Generic;
using Domain.MeetingRooms;
using Domain.Usages;
using InMemoryInfrastructure;

namespace Test
{
    public class MeetingRoomsTest
    {
        IUsageRepository repository = new InMemoryUsageRepository();
        [Fact]
        public void 会議室Aのステータスを入室にする()
        {
            MeetingRoom.A.EnteringRoom();
            Assert.Equal(RoomStatus.USE, MeetingRoom.A.Status);
        }
        [Fact]
        public void 会議室Aに入室したら利用記録が登録されていること() 
        {
            MeetingRoom.A.EnteringRoom();
            var now = DateTime.Now;

            // とりあえず、MinValueにした
            Usage usage = repository.Find(MeetingRoom.A, DateTime.Now, DateTime.MinValue);

            
            Assert.Equal(usage.startDateTime.Date, now.Date);
            Assert.Equal(usage.startDateTime.Hour, now.Hour);
            Assert.Equal(usage.startDateTime.Minute, now.Minute);

            Assert.Equal(usage.endDateTime, DateTime.MinValue);

        }
    }
}