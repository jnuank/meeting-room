using System;
using Xunit;
using modeling_mtg_room.Domain.Reserve;
using Moq;
using System.Collections.Generic;

namespace modeling_mtg_room.Test
{
    public class TestReservedTime
    {
        [Fact]
        public void ゼロ秒であること()
        {
            ReservedTime rt = new ReservedTime(2019, 12,31,12,30);
            Assert.Equal(0, rt.Value.Second);
        }

        [Fact]
        public void 等値テスト()
        {
            ReservedTime rt = new ReservedTime(2019, 12,31,12,15);
            ReservedTime rt2 = new ReservedTime(2019, 12,31,12,15);

            Assert.True(rt.Equals(rt2));
        }

        [Fact]
        public void 不等値テスト()
        {
            ReservedTime rt = new ReservedTime(2019,12,31,10,30);
            ReservedTime rt2 = new ReservedTime(2019,12,31,18,45);

            Assert.False(rt.Equals(rt2));
        }

        [Theory]
        [InlineData(19)]
        [InlineData(31)]
        [InlineData(47)]
        [InlineData(23)]
        [Trait("Category", "FailPattern")]
        public void 十五分単位以外の値の場合は生成できないこと(int minute)
        {
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTime rt = new ReservedTime(2019,12,31, 12, minute);
            });
        }
        [Theory]
        [InlineData(15)]
        [InlineData(30)]
        [InlineData(45)]
        [InlineData(0)]
        [Trait("Category", "UsuallyPattern")]
        public void 十五分単位が生成できること(int minute)
        {
            ReservedTime rt = new ReservedTime(2019,12,31, 12, minute);
            Assert.Equal(minute, rt.Value.Minute);
        }
        [Fact]
        public void 三十一日後の日付で予約をしようとするとエラー()
        {
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 1, 0,0,0));
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTime rts = new ReservedTime(2020, 2, 1, 10,15, dateTime.Object);
            });
        }
        [Fact]
        public void 三十日後の日付で予約をしようとするとエラーにならない()
        {
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                .Returns(new DateTime(2020, 1, 1, 0,0,0));
            ReservedTime rts = new ReservedTime(2020, 1, 31, 14,0, dateTime.Object);
            Assert.Equal(0, rts.Value.Minute);
        }

        [Theory]
        [InlineData(2020,1,1,9,0)]
        [InlineData(2020,1,1,9,45)]
        [InlineData(2020,1,1,20,0)]
        [InlineData(2020,1,1,19,15)]
        [InlineData(2020,1,1,19,30)]
        [InlineData(2020,1,1,19,45)]
        public void 十時から十九時まで以外の予約にした場合エラーとなる(int year, int month, int day, int hour, int minute)
        {
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTime rt = new ReservedTime(year, month, day, hour, minute);
            });
        }
    }
}