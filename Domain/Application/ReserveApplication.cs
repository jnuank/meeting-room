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

            // todo: 予約の重複を確認するために、重複しているであろうデータの抽出を行う必要がある
            //if(reserveService.IsOverlap(reserve))
            //    throw new Exception("予約が重なっています");
            
            await repository.SaveAsync(reserve);
            
            return reserve.Id.Value;
        }

        /// <summary>
        /// 予約情報を探す
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ReserveModel> FindReserveAsync(string id)
        {
            Reserve reserve = await repository.FindAsync(new ReserveId(id));
            return new ReserveModel 
            {
                Id              = reserve.Id.Value,
                ReserverId      = reserve.ReserverId.Value,
                ReserveOfNumber = reserve.ReserverOfNumber.ValueString,
                StartDate       = reserve.TimeSpan.StartDateString,
                EndDate         = reserve.TimeSpan.EndDateString,
                Room            = reserve.Room.ToString()
            };
        }

        /// <summary>
        /// 予約のキャンセルをする
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task CancelReserveAsync(string id)
        {
            await repository.DeleteAsync(new ReserveId(id));
        }

        public async Task<string> ModifyReserveAsync(string id,
                                                            string room,
                                                            int startYear, int startMonth, int startDay, int startHour, int startMinute,
                                                            int endYear, int endMonth, int endDay, int endHour, int endMinute,
                                                            int reserverOfNumber,
                                                            string reserverId)
        {
            // todo:ここの動きはおかしくて、
            // 本来はデータをリポジトリから取得してきたあとに、
            // Changeメソッドなど呼んで修正をするべき
            
            bool data = await repository.ExistsAsync(new ReserveId(id));

            //todo: これは正常系の失敗なので、どう扱うべきか悩ましい            
            if(!data)
                throw new ApplicationException("指定した予約が存在しません");

            MeetingRooms mtgRoom;
            if(!Enum.TryParse(room, true, out mtgRoom))
                throw new ApplicationException("指定された会議室が存在しません");

            var startTime = new ReservedTime(startYear, startMonth, startDay, startHour, startMinute, this.dateTime);
            var endTime   = new ReservedTime(endYear, endMonth, endDay, endHour, endMinute, this.dateTime);

            var reserve = new Reserve(new ReserveId(id),
                                        mtgRoom,
                                        new ReservedTimeSpan(startTime, endTime),
                                        new ReserverOfNumber(reserverOfNumber),
                                        new ReserverId(reserverId));
            
            // todo: ここで重複チェックをする
            // memo: 重複以外のチェックは、ドメインオブジェクトの中で担保ができている状態
            await repository.SaveAsync(reserve);

            return id;
        }
    }
}