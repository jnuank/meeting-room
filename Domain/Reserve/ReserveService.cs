
using System;
using System.Linq;

namespace modeling_mtg_room.Domain.Reserve
{
    internal class ReserveService
    {
        private IReserveRepository repository;

        public ReserveService(IReserveRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 予約しようとしているものが重なっているかどうかの確認する
        /// </summary>
        /// <param name="reserve"></param>
        /// <returns></returns>
        public bool IsOverlap(Reserve reserve)
        {
            MeetingRooms room = reserve.Room;
            var list = repository.FindOfRoom(room);
            return list.Any(x => reserve.TimeSpan.IsOverlap(x.TimeSpan));
            // todo: 仮実装
        }
    }
}