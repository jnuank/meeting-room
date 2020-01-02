using System;
using Xunit;
using modeling_mtg_room.Domain.Reserve;
using Moq;
using InMemoryInfrastructure;

namespace modeling_mtg_room.Test
{
    public class TestUsecase
    {
        [Fact]
        public void 会議室を予約したら予約が発生する()
        {
            var repository = new InMemoryReserveRepository();
            
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 20, 0,0,0));

            string room = "a";
            int reserverOfNumber = 5;
            string reserverId = "abcdefg";

            var usecase = new ReserveApplication(repository, dateTime.Object);
            var id = usecase.ReserveMeetingRoom(room,
                                        2020, 2, 1, 10, 0,
                                        2020, 2, 1, 13, 15,
                                        reserverOfNumber,
                                        reserverId);
            var よやく = repository.Find(id);
            Assert.NotNull(よやく);
            Assert.True(よやく.Id == id);

        }
        [Fact]
        public void 重なる予約ををした場合はエラーとなる()
        {
            var repository = new InMemoryReserveRepository();
            
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 20, 0,0,0));

            string room = "a";
            int reserverOfNumber = 5;
            string reserverId = "abcdefg";

            var usecase = new ReserveApplication(repository, dateTime.Object);
            var id = usecase.ReserveMeetingRoom(room,
                                        2020, 2, 1, 10, 0,
                                        2020, 2, 1, 13, 15,
                                        reserverOfNumber,
                                        reserverId);

            var よやく = repository.Find(id);
            string room2 = "a";
            int reserverOfNumber2 = 3;
            string reserverId2 = "eeeeee";

            Assert.Throws<Exception>(() => {
                var id2 = usecase.ReserveMeetingRoom(room2,
                                                    2020, 2, 1, 13, 0,
                                                    2020, 2, 1, 15, 45,
                                                    reserverOfNumber2,
                                                    reserverId2);
            });
        }
    }
    // 被ってしまったらエラーとするテストを見る
}
