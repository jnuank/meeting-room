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
            GetObjectRequest request = new GetObjectRequest()
            {
                BucketName = "meeting-room",
                Key = id.Value,
            };
            try {
                var obj = client.GetObjectAsync(request);
                Console.WriteLine(obj);
                return null;
            }catch (Exception ex){
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
            string data = DynamicJson.Serialize(reserve);
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
