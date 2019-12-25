using System;
using Xunit;
using modeling_mtg_room.Usecase;
using modeling_mtg_room.Model;

namespace modeling_mtg_room.Test
{
    public class Test予約時間
    {
        [Fact]
        public void 十時から十二時で登録されたら二時間となること()
        {
            予約情報 じょうほう = new 予約情報(new DateTime(2019, 12, 25, 10,0,0), new DateTime(2019, 12, 25, 12,0,0));
            Assert.Equal(2.0, じょうほう.時間);
        }
    }
}
