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
            予約情報 じょうほう = new 予約情報(10, 12);
            予約 よやく = Usecase.Usecase.会議室を予約する(じょうほう);
            Assert.NotNull(よやく);
        }
        // [Fact]
        // public void 十時から十二時で予約したら二時間の予約が出来ること()
        // {
        //     予約情報 じょうほう = new 予約情報();
        //     予約 よやく = Usecase.Usecase.会議室を予約する(じょうほう);
        //     Assert.Equal(2.0, よやく.予約時間.Value);
        // }
    }
}
