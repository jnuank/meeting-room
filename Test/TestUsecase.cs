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
        [Theory]
        [InlineData(2020, 2, 1, 12, 45,
                    2020, 2, 1, 15, 45)]
        [InlineData(2020, 2, 1, 12, 0,
                    2020, 2, 1, 12, 30)]
        [InlineData(2020, 2, 1, 10, 0,
                    2020, 2, 1, 11, 15)]
        [Trait("Category", "FailPattern")]
        public void 重なる予約ををした場合はエラーとなる(int startYear, int startMonth, int startDay, int startHour, int startMinute,
                                                    int endYear, int endMonth, int endDay, int endHour, int endMinute)
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
                                        2020, 2, 1, 11, 0,
                                        2020, 2, 1, 13, 0,
                                        reserverOfNumber,
                                        reserverId);

            var よやく = repository.Find(id);
            string room2 = "a";
            int reserverOfNumber2 = 3;
            string reserverId2 = "eeeeee";

            Assert.Throws<Exception>(() => {
                var id2 = usecase.ReserveMeetingRoom(room2,
                                                    startYear, startMonth, startDay, startHour, startMinute,
                                                    endYear, endMonth, endDay, endHour, endMinute,
                                                    reserverOfNumber2,
                                                    reserverId2);
            });
        }
        [Theory]
        [InlineData(2020, 2, 1, 10, 0,
                    2020, 2, 1, 11, 0)]
        [InlineData(2020, 2, 1, 13, 0,
                    2020, 2, 1, 13, 15)]
        [InlineData(2020, 2, 1, 17, 0,
                    2020, 2, 1, 19, 0)]
        [Trait("Category", "UsuallyPattern")]
        public void 重ならずに予約ができること(int startYear, int startMonth, int startDay, int startHour, int startMinute,
                                            int endYear, int endMonth, int endDay, int endHour, int endMinute)
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
                                        2020, 2, 1, 11, 0,
                                        2020, 2, 1, 13, 0,
                                        reserverOfNumber,
                                        reserverId);

            string room2 = "a";
            int reserverOfNumber2 = 3;
            string reserverId2 = "eeeeee";

            var id2 = usecase.ReserveMeetingRoom(room2,
                                                startYear, startMonth, startDay, startHour, startMinute,
                                                endYear, endMonth, endDay, endHour, endMinute,
                                                reserverOfNumber2,
                                                reserverId2);


            var よやく = repository.Find(id2);
            Assert.NotNull(よやく);
            Assert.True(よやく.Id == id2);
        }
    }
}
