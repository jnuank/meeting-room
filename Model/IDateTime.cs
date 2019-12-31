using System;

namespace modeling_mtg_room.Model
{
    /// <summary>
    /// 現在日時を取得するためのインターフェース
    /// </summary>
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}