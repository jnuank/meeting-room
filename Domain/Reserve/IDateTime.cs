using System;

namespace modeling_mtg_room.Domain.Reserve
{
    /// <summary>
    /// 現在日時を取得するためのインターフェース
    /// </summary>
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}