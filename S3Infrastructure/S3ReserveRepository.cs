using System;
using System.Collections.Generic;
using modeling_mtg_room.Domain.Reserve;
using System.Linq;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon;
using System.Threading.Tasks;
using Amazon.S3.Transfer;
using Codeplex.Data;
using System.IO;
using S3Infrastructure.Models;

namespace S3Infrastructure
{
    public class S3ReserveRepository : IReserveRepository
    {
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APNortheast1; 
        private readonly AmazonS3Client client;
        public S3ReserveRepository()
        {
            client = new AmazonS3Client(bucketRegion);

        }
        public Reserve Find(ReserveId id)
        {
            throw new NotImplementedException();
        }

        public async Task<Reserve> FindAsync(ReserveId id)
        {
            GetObjectRequest request = new GetObjectRequest()
            {
                BucketName = "meeting-room-bucket",
                Key = id.Value,
            };
            try {
                var response = await client.GetObjectAsync(request);
                Console.WriteLine(response);

                using(StreamReader reader = new StreamReader(response.ResponseStream))
                {
                    string contents = reader.ReadToEnd();
                    var reserve = DynamicJson.Parse(contents);
                    Console.WriteLine(reserve);

                    DateTime start = DateTime.Parse(reserve["StartDate"], null, System.Globalization.DateTimeStyles.RoundtripKind);
                    DateTime end   = DateTime.Parse(reserve["EndDate"]  , null, System.Globalization.DateTimeStyles.RoundtripKind);

                    MeetingRooms mtgRoom;
                    Enum.TryParse(reserve["Room"], true, out mtgRoom);

                    var startTime = new ReservedTime(start.Year, start.Month, start.Day, start.Hour, start.Minute);
                    var endTime = new ReservedTime(end.Year, end.Month, end.Day, end.Hour, end.Minute);
                    var timeSpan = new ReservedTimeSpan(startTime, endTime);
                    var reserver = new ReserverOfNumber(int.Parse(reserve["ReserveOfNumber"]));
                    var reserverId = new ReserverId(reserve["ReserverId"]);

                    return new Reserve(id, mtgRoom, timeSpan, reserver, reserverId);

                }
            }catch (Exception ex){
                Console.WriteLine("Getエラー");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        public IEnumerable<Reserve> FindOfRoom(MeetingRooms room)
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = "meeting-room", 
            };
            try {
                var obj = client.GetObjectAsync(request);
                Console.WriteLine(obj);
                
                return null;
            }catch (Exception ex){
                Console.WriteLine("Getエラー");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw ex;
            } 
        }

        public void Save(Reserve reserve)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(Reserve reserve)
        {
            Console.WriteLine("SaveAsync");
            ReserveModel model = new ReserveModel
            {
                ReserverId = reserve.ReserverId.Value,
                Room = reserve.Room.ToString(),
                StartDate = reserve.TimeSpan._start.Value.ToString("o"),
                EndDate = reserve.TimeSpan._end.Value.ToString("o"),
                Id = reserve.Id.Value,
                ReserveOfNumber = reserve.ReserverOfNumber.Value.ToString()
            };

            string data = DynamicJson.Serialize(model);
            Console.WriteLine(data);
            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = "meeting-room-bucket",
                Key = reserve.Id.Value,
                ContentType = "text/text",
                ContentBody = data
                //ContentBody = "Hello World!"
            };
            Console.WriteLine(request.ToString());
            try { 
                await client.PutObjectAsync(request);
                return;
            } catch (Exception ex){
                Console.WriteLine("Putエラー");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
