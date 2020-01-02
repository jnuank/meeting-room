using System;
using Xunit;
using modeling_mtg_room.Domain.Reserve;
using Moq;
using System.Collections.Generic;

namespace modeling_mtg_room.Test
{
    public class TestReservedTimeSpan
    {
        public static IEnumerable<object[]> UsuallySource()
        {
            yield return new object[] { new ReservedTime( 2019, 12, 25, 10,0), new ReservedTime(2019, 12, 25, 12,0), 2.0 };
            yield return new object[] { new ReservedTime( 2019, 12, 25, 10,15), new ReservedTime(2019, 12, 25, 10,30), 0.25 };
            yield return new object[] { new ReservedTime( 2019, 12, 25, 10,15), new ReservedTime(2019, 12, 25, 10,45), 0.5};
            yield return new object[] { new ReservedTime( 2019, 12, 25, 10,0), new ReservedTime(2019, 12, 25, 19,0), 9.0};
        }

        [Theory]
        [MemberData(nameof(UsuallySource))]
        [Trait("Category", "UsuallyPattern")]
        public void 開始時間と終了時間を入力したら数値時間を取得できること(ReservedTime start, ReservedTime end, double expected)
        {
            ReservedTimeSpan じょうほう = new ReservedTimeSpan(start, end);
            Assert.Equal(expected, じょうほう.TimeOfNumber);
        }

        public static IEnumerable<object[]> FailSource()
        {
            yield return new object[] { new ReservedTime(2019,12,29, 13,15), new ReservedTime(2019,12,29,10,0) };
        }
        [Theory]
        [MemberData(nameof(FailSource))]
        [Trait("Category", "FailPattern")]
        public void 開始時間が終了時間より未来日時の場合はエラー(ReservedTime start, ReservedTime end)
        {
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(start, end);
            });
        }

        public static IEnumerable<object[]> ZeroFailSource()
        {
            yield return new object[] { new ReservedTime(2019,12,29, 13,15), new ReservedTime(2019,12,29,13,15) };
            yield return new object[] { new ReservedTime(2020,1,2, 17,0), new ReservedTime(2020,1,2,17,0) };
        }
        [Theory]
        [MemberData(nameof(ZeroFailSource))]
        [Trait("Category", "FailPattern")]
        public void ゼロ時間分の予約はエラーとなる(ReservedTime start, ReservedTime end)
        {
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(start, end);
            });
        }

        public static IEnumerable<object[]> SameDayFailSource()
        {
            yield return new object[] { new ReservedTime(2019,12,29, 13,15), new ReservedTime(2019,12,30,13,15) };
            yield return new object[] { new ReservedTime(2020,1,2, 17,0), new ReservedTime(2020,2,2,17,0) };
        }
        [Theory]
        [MemberData(nameof(SameDayFailSource))]
        [Trait("Category", "FailPattern")]
        public void 開始時間と終了時間で別日付だった場合はエラーとする(ReservedTime start, ReservedTime end)
        {
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(start, end);
            });
        }
        //todo:等値テストや、重なっていないかどうかのテストも必要

    }
}
