using System;
using Xunit;
using modeling_mtg_room.Usecase;
using modeling_mtg_room.Model;

namespace modeling_mtg_room.Test
{
    public class TestUsecase
    {
        [Fact]
        public void 会議室を予約したら予約が発生する()
        {
            予約情報 じょうほう = new 予約情報();
            予約 よやく = Reserve.会議室を予約する(じょうほう);
            Assert.NotNull(よやく);
        }
    }
}
