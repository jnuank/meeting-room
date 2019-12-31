using System;
using Xunit;
using modeling_mtg_room.Usecase;
using modeling_mtg_room.Model;
using Moq;

namespace modeling_mtg_room.Test
{
    public class TestUsecase
    {
        [Fact]
        public void 会議室を予約したら予約が発生する()
        {
            string room = "a";
            DateTime start = new DateTime(2020, 2, 1, 10,0,0);
            var end = new DateTime(2020, 2, 1, 13, 15, 20);
            int reserverOfNumber = 5;
            string reserverId = "abcdefg";
            予約 よやく = Usecase.Usecase.会議室を予約する(room,
                                                        start,
                                                        end,
                                                        reserverOfNumber,
                                                        reserverId);
            Assert.NotNull(よやく);
        }
    }
}
