using System;
using Xunit;
using modeling_mtg_room.Usecase;
using modeling_mtg_room.Model;

namespace modeling_mtg_room.Test
{
    public class TestReservedTimeSpan
    {
        [Fact]
        public void 十時から十二時で登録されたら二時間となること()
        {
            ReservedTimeSpan じょうほう = new ReservedTimeSpan(new DateTime(2019, 12, 25, 10,0,0), new DateTime(2019, 12, 25, 12,0,0));
            Assert.Equal(2.0, じょうほう.TimeOfNumber);
        }
        [Fact]
        public void 十時十五分から十時三十分で登録されたら十五分となること()
        {
            ReservedTimeSpan じょうほう = new ReservedTimeSpan(new DateTime(2019, 12, 25, 10,15,0), new DateTime(2019, 12, 25, 10,30,0));
            Assert.Equal(0.25, じょうほう.TimeOfNumber);
        }
        [Fact]
        public void 十五分単位で入力されていなかったらエラーとなること()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2019,12,29, 10,11,0), new DateTime(2019,12,29,10,15,10));
            });
        }
        [Fact]
        public void 十五分単位で入力されていなかったらエラーとなること２()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2019,12,29, 10,45,0), new DateTime(2019,12,29,10,39,10));
            });
        }
        [Fact]
        public void StatがEndより未来の時間だったらエラーとなること()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2019,12,29, 13,15,0), new DateTime(2019,12,29,10,00,00));
            });
        }
        [Fact]
        public void StatがEndが同じだった場合エラーとならずに数値時間０分となること()
        {
            ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2019, 12, 25, 10,15,0), new DateTime(2019, 12, 25, 10,15,0));
            Assert.Equal(0.0, rts.TimeOfNumber);
        }
    }
}
