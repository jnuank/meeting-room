using System;
using System.Threading.Tasks;
using modeling_mtg_room.Domain.Reserves;
using modeling_mtg_room.Domain.Repository;
using modeling_mtg_room.Domain.Application.Models;

namespace modeling_mtg_room.Domain.Application
{
    public class ReserveApplication
    {
        private readonly IDateTime dateTime;
        private readonly IReserveRepository repository;
        private readonly ReserveService reserveService;
        public ReserveApplication(IReserveRepository repository,  IDateTime dateTime = null)
        {
            // デフォルトではサーバが保持する時間を使用する
            this.dateTime = dateTime ?? new ServerDateTime();
            this.repository = repository;
            this.reserveService = new ReserveService(repository);
        }
        public string ReserveMeetingRoom(string room,
                                            int startYear, int startMonth, int startDay, int startHour, int startMinute,
                                            int endYear, int endMonth, int endDay, int endHour, int endMinute,
                                            int reserverOfNumber,
                                            string reserverId)
        {
            MeetingRooms mtgRoom;
            if(!Enum.TryParse(room, true, out mtgRoom))
                throw new ApplicationException("指定された会議室が存在しません");

            var startTime = new ReservedTime(startYear, startMonth, startDay, startHour, startMinute, this.dateTime);
            var endTime   = new ReservedTime(endYear, endMonth, endDay, endHour, endMinute, this.dateTime);
            var timeSpan  = new ReservedTimeSpan(startTime, endTime);
            var reserver  = new ReserverOfNumber(reserverOfNumber);
            var id        = new ReserverId(reserverId);

            var reserve = new Reserve(mtgRoom, timeSpan, reserver, id);

            if(reserveService.IsOverlap(reserve))
                throw new Exception("予約が重なっています");
            
            repository.Save(reserve);

            return reserve.Id.Value;
        }

        // memo:ユースケースは、わりと手続き的な処理を見通せるようにしておきたい意図があるので、
        // 必要以上に重複コードを排除して、共通化とかにしない方がいいかと思い、このままにしておく
        public string ReserveMeetingRoom(string room,
                                            int startYear, int startMonth, int startDay, int startHour, int startMinute,
                                            int timeBlock,
                                            int reserverOfNumber,
                                            string reserverId)
        {
            MeetingRooms mtgRoom;
            if(!Enum.TryParse(room, true, out mtgRoom))
                throw new ApplicationException("指定された会議室が存在しません");

            var startTime = new ReservedTime(startYear, startMonth, startDay, startHour, startMinute, this.dateTime);
            var timeSpan  = new ReservedTimeSpan(startTime, timeBlock);
            var reserver  = new ReserverOfNumber(reserverOfNumber);
            var id        = new ReserverId(reserverId);

            var reserve = new Reserve(mtgRoom, timeSpan, reserver, id);

            if(reserveService.IsOverlap(reserve))
                throw new Exception("予約が重なっています");
            
            repository.Save(reserve);

            return reserve.Id.Value;
        }
        /// <summary>
        /// 非同期用メソッド
        /// </summary>
        /// <returns></returns>
        public async Task<string> ReserveMeetingRoomAsync(string room,
                                            int startYear, int startMonth, int startDay, int startHour, int startMinute,
                                            int endYear, int endMonth, int endDay, int endHour, int endMinute,
                                            int reserverOfNumber,
                                            string reserverId)
        {
            MeetingRooms mtgRoom;
            if(!Enum.TryParse(room, true, out mtgRoom))
                throw new ApplicationException("指定された会議室が存在しません");

            var startTime = new ReservedTime(startYear, startMonth, startDay, startHour, startMinute, this.dateTime);
            var endTime = new ReservedTime(endYear, endMonth, endDay, endHour, endMinute, this.dateTime);
            var timeSpan = new ReservedTimeSpan(startTime, endTime);
            var reserver = new ReserverOfNumber(reserverOfNumber);
            var id = new ReserverId(reserverId);

            var reserve = new Reserve(mtgRoom, timeSpan, reserver, id);

            //if(reserveService.IsOverlap(reserve))
            //    throw new Exception("予約が重なっています");
            
            await repository.SaveAsync(reserve);
            
            return reserve.Id.Value;
        }

        public async Task<ReserveModel> FindReserve(string id)
        {
            Reserve reserve = await repository.FindAsync(new ReserveId(id));
            return new ReserveModel 
            {
                Id = reserve.Id.Value,
                ReserverId = reserve.ReserverId.Value,
                ReserveOfNumber = reserve.ReserverOfNumber.Value.ToString(),
                StartDate = reserve.TimeSpan._start.Value.ToString("o"),
                EndDate = reserve.TimeSpan._end.Value.ToString("o"),
                Room = reserve.Room.ToString()
            };
        }

    }
}