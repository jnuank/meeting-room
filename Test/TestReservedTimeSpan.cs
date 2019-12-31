using System;
using Xunit;
using modeling_mtg_room.Usecase;
using modeling_mtg_room.Model;
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
            yield return new object[] { new ReservedTime( 2019, 12, 25, 10,15), new ReservedTime(2019, 12, 25, 10,15), 0};
            yield return new object[] { new ReservedTime( 2019, 12, 25, 10,0), new ReservedTime(2019, 12, 25, 19,0), 9.0};
            // yield return new object[] { 1, 0, 1 };
            // yield return new object[] { 1, 1, 0 };
            // yield return new object[] { 2, 1, 1 };
        }

        public static IEnumerable<object[]> FailSource()
        {
            yield return new object[] { new ReservedTime(2019,12,29, 13,15), new ReservedTime(2019,12,29,10,0) };
        }

        [Theory]
        [MemberData(nameof(UsuallySource))]
        [Trait("Category", "UsuallyPattern")]
        public void 開始時間と終了時間を入力したら数値時間を取得できること(ReservedTime start, ReservedTime end, double expected)
        {
            ReservedTimeSpan じょうほう = new ReservedTimeSpan(start, end);
            Assert.Equal(expected, じょうほう.TimeOfNumber);
        }
        [Theory]
        [MemberData(nameof(FailSource))]
        [Trait("Category", "FailPattern")]
        public void 不正パターン(ReservedTime start, ReservedTime end)
        {
            Assert.Throws<ArgumentException>(() => 
            {                
                ReservedTimeSpan rts = new ReservedTimeSpan(start, end);
            });
        }
//         [Fact]
//         public void 三十一日後の日付で予約をしようとするとエラー()
//         {
//             var dateTime = new Mock<IDateTime>();
//             dateTime.Setup(d => d.Now)
//                 .Returns(new DateTime(2020, 1, 1, 0,0,0));
//             Assert.Throws<ArgumentException>(() => 
//             {                
//                 ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 2, 1, 10,15,0), new DateTime(2020, 2, 1, 10,15,0), dateTime.Object);
//             });
//         }
//         [Fact]
//         public void 三十日後の日付で予約をしようとするとエラーにならない()
//         {
//             var dateTime = new Mock<IDateTime>();
//             dateTime.Setup(d => d.Now)
//                 .Returns(new DateTime(2020, 1, 1, 0,0,0));

//             ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 1, 31, 14,0,0), new DateTime(2020, 1, 31, 15,0,0), dateTime.Object);
//             Assert.Equal(1.0, rts.TimeOfNumber);
//         }
//         [Fact]
//         public void 十時から十九時まで以外の予約にした場合エラーとなる()
//         {
//             var dateTime = new Mock<IDateTime>();
//             dateTime.Setup(d => d.Now)
//                 .Returns(new DateTime(2020, 1, 1, 0,0,0));
 
//             Assert.Throws<ArgumentException>(() => 
//             {                
//                 ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 1, 31, 9, 0, 0),
//                                                             new DateTime(2020, 1, 31, 15, 0, 0),
//                                                             dateTime.Object);
//             });
//         }
//         [Fact]
//         public void 十時から十九時まで以外の予約にした場合エラーとなる２()
//         {
//             var dateTime = new Mock<IDateTime>();
//             dateTime.Setup(d => d.Now)
//                 .Returns(new DateTime(2020, 1, 1, 0,0,0));

// 　           Assert.Throws<ArgumentException>(() => 
//             {                
//                 ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 1, 31, 20, 0, 0),
//                                                             new DateTime(2020, 1, 31, 15, 0, 0),
//                                                             dateTime.Object);
//             });
//         }
//         [Fact]
//         public void 十時から十九時まで以外の予約にした場合エラーとなる３()
//         {
//             var dateTime = new Mock<IDateTime>();
//             dateTime.Setup(d => d.Now)
//                 .Returns(new DateTime(2020, 1, 1, 0,0,0));
//             Assert.Throws<ArgumentException>(() => 
//             {                
//                 ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 1, 31, 10, 0, 0),
//                                                             new DateTime(2020, 1, 31, 9, 0, 0),
//                                                             dateTime.Object);
//             });
//         }

//         [Fact]
//         public void 十時から十九時まで以外の予約にした場合エラーとなる４()
//         {
//             var dateTime = new Mock<IDateTime>();
//             dateTime.Setup(d => d.Now)
//                 .Returns(new DateTime(2020, 1, 1, 0,0,0));
//             Assert.Throws<ArgumentException>(() => 
//             {                
//                 ReservedTimeSpan rts = new ReservedTimeSpan(new DateTime(2020, 1, 31, 10, 0, 0),
//                                                             new DateTime(2020, 1, 31, 20, 0, 0),
//                                                             dateTime.Object);
//             });
//         }

        //todo:等値テストや、重なっていないかどうかのテストも必要

    }
}
