using System;
using modeling_mtg_room.Model;

namespace modeling_mtg_room.Usecase
{
    public class Reserve
    {
        private Reserve()
        {

        }
        public static 予約 会議室を予約する(予約情報 よやく)
        {
            return new 予約();
        }
    }
}