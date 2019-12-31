using System;
using Xunit;
using modeling_mtg_room.Model;
using Moq;

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

        [Theory]
        [InlineData(19)]
        [InlineData(31)]
        [InlineData(47)]
        [InlineData(23)]
        [Trait("Category", "不正値パターン")]
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
        [Trait("Category", "正常値パターン")]
        public void 十五分単位が生成できること(int minute)
        {
            ReservedTime rt = new ReservedTime(2019,12,31, 12, minute);
            Assert.Equal(minute, rt.Value.Minute);
        }
        
    }
}