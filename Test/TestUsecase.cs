using System;
using Xunit;
using modeling_mtg_room.Model;
using modeling_mtg_room.Usecase;
using Moq;

namespace modeling_mtg_room.Test
{
    public class TestUsecase
    {
        [Fact]
        public void 会議室を予約したら予約が発生する()
        {
            //todo: ここでテスト用のInMemoryリポジトリを入れる

            
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 20, 0,0,0));

            string room = "a";
            int reserverOfNumber = 5;
            string reserverId = "abcdefg";

            var usecase = new ReserveApplication(dateTime.Object);
            Reserve よやく = usecase.ReserveMeetingRoom(room,
                                                        2020, 2, 1, 10, 0,
                                                        2020, 2, 1, 13, 15,
                                                        reserverOfNumber,
                                                        reserverId);
            Assert.NotNull(よやく);
            Assert.Equal(MeetingRooms.A, よやく.Room);
            Assert.Equal("2020/02/01 10:00:00", よやく.TimeSpan._start.Value.ToString("yyyy/MM/dd HH:mm:ss"));
            Assert.Equal("2020/02/01 13:15:00", よやく.TimeSpan._end.Value.ToString("yyyy/MM/dd HH:mm:ss"));
            Assert.Equal(5, よやく.ReserverOfNumber.Value);
            Assert.Equal("abcdefg", よやく.ReserverId.Value);


            
        }
    }
    // 被ってしまったらエラーとするテストを見る
}
