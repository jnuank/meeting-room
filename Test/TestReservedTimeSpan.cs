using System;
using Xunit;
using modeling_mtg_room.Usecase;
using modeling_mtg_room.Model;
using Moq;

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
            ReservedTimeSpan じょうほう = new ReservedTimeSpan(new DateTime(2019, 12, 25, 10,15,59), new DateTime(2019, 12, 25, 10,30,10));
            Assert.Equal(0.25, じょうほう.TimeOfNumber);
        }
        [Fact]
        public void 十五分単位で入力されていなかったらエラーとなること()
        {
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2019,12,29, 10,11,0), new DateTime(2019,12,29,10,15,10));
            });
        }
        [Fact]
        public void 十五分単位で入力されていなかったらエラーとなること２()
        {
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2019,12,29, 10,45,0), new DateTime(2019,12,29,10,39,10));
            });
        }
        [Fact]
        public void StatがEndより未来の時間だったらエラーとなること()
        {
            Assert.Throws<ArgumentException>(() => 
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
        [Fact]
        public void 三十一日後の日付で予約をしようとするとエラー()
        {
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 1, 0,0,0));
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 2, 1, 10,15,0), new DateTime(2020, 2, 1, 10,15,0), dateTime.Object);
            });
        }
        [Fact]
        public void 三十日後の日付で予約をしようとするとエラーにならない()
        {
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 1, 0,0,0));

            ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 1, 31, 14,0,0), new DateTime(2020, 1, 31, 15,0,0), dateTime.Object);
            Assert.Equal(1.0, rts.TimeOfNumber);
        }
        [Fact]
        public void 十時から十九時まで以外の予約にした場合エラーとなる()
        {
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 1, 0,0,0));
 
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 1, 31, 9, 0, 0),
                                                            new DateTime(2020, 1, 31, 15, 0, 0),
                                                            dateTime.Object);
            });
        }
        [Fact]
        public void 十時から十九時まで以外の予約にした場合エラーとなる２()
        {
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 1, 0,0,0));

　           Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 1, 31, 20, 0, 0),
                                                            new DateTime(2020, 1, 31, 15, 0, 0),
                                                            dateTime.Object);
            });
        }
        [Fact]
        public void 十時から十九時まで以外の予約にした場合エラーとなる３()
        {
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 1, 0,0,0));
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 1, 31, 10, 0, 0),
                                                            new DateTime(2020, 1, 31, 9, 0, 0),
                                                            dateTime.Object);
            });
        }

        [Fact]
        public void 十時から十九時まで以外の予約にした場合エラーとなる４()
        {
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 1, 0,0,0));
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 1, 31, 10, 0, 0),
                                                            new DateTime(2020, 1, 31, 20, 0, 0),
                                                            dateTime.Object);
            });
        }

        //todo:等値テストや、重なっていないかどうかのテストも必要

    }
}
