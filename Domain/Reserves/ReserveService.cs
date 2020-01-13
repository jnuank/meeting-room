
using System;
using System.Linq;
using Domain.Repository;

namespace Domain.Reserves
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
        }
    }
}