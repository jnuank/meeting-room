using System;

namespace modeling_mtg_room.Domain.Reserves
{
    /// <summary>
    /// 現在日時を取得する
    /// </summary>
    public class ServerDateTime : IDateTime

    {
        public DateTime Now => DateTime.Now;
    }
}