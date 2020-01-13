using System;

namespace Domain.Reserves
{
    /// <summary>
    /// 現在日時を取得する
    /// </summary>
    public class ServerDateTime : IDateTime

    {
        public DateTime Now => DateTime.Now;
    }
}