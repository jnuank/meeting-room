using System;
using modeling_mtg_room.Model;

namespace modeling_mtg_room.Usecase
{
    public class Usecase
    {
        private readonly IDateTime dateTime;
        
        private Usecase(IDateTime dateTime = null)
        {
            // デフォルトではサーバが保持する時間を使用する
            this.dateTime = dateTime ?? new ServerDateTime();
        }
        public static 予約 会議室を予約する(string room,
                                        DateTime start,
                                        DateTime end,
                                        int reserverOfNumber,
                                        string reserverId)
        {
            MeetingRooms mtgRoom;
            if(!Enum.TryParse(room, true, out mtgRoom))
                throw new ApplicationException("指定された会議室が存在しません");
            
            return new 予約();
        }
            //todo: 入れた時間が、バッティングしていないかどうかをチェックする必要がある
    }
}