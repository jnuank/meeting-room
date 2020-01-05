using System;
using Xunit;
using modeling_mtg_room.Domain.Reserves;
using modeling_mtg_room.Domain.Application;
using modeling_mtg_room.Domain.Repository;
using Moq;
using InMemoryInfrastructure;

namespace modeling_mtg_room.Test
{
    public class TestUsecase
    {
        [Fact]
        public void 会議室を予約する()
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
            var よやく = repository.Find(new ReserveId(id));
            Assert.NotNull(よやく);
            Assert.True(よやく.Id.Equals(new ReserveId(id)));
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

            var よやく = repository.Find(new ReserveId(id));
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


            var よやく = repository.Find(new ReserveId(id2));
            Assert.NotNull(よやく);
            Assert.True(よやく.Id.Equals(new ReserveId(id2)));
        }
        [Theory]
        [InlineData(2020, 2, 1, 10, 0, 6, 1.5)]
        [InlineData(2020, 2, 1, 10, 0, 10, 2.5)]
        public void 会議室を予約するの別ユースケース(int year, int month, int day, int hour, int minute, int timeBlock, double expected)
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
                                        year, month, day, hour, minute,
                                        timeBlock,
                                        reserverOfNumber,
                                        reserverId);
            var よやく = repository.Find(new ReserveId(id));
            Assert.NotNull(よやく);
            Assert.True(よやく.Id.Equals(new ReserveId(id)));
            Assert.Equal(expected, よやく.TimeSpan.TimeOfNumber);
        }
    }
}
