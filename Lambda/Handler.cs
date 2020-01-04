using Amazon.Lambda.Core;
using System;
using modeling_mtg_room.Domain.Reserve;
using InMemoryInfrastructure;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Codeplex.Data;


[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {
      public async Task<LambdaResponse> Hello(Stream input, ILambdaContext context)
      {

          var repository = new InMemoryReserveRepository();
          // ユースケースを実行する
          using (var reader = new StreamReader(input))
          {
              var val = await reader.ReadToEndAsync();

              var obj = DynamicJson.Parse(val);
              var body = DynamicJson.Parse(obj["body"]);
              Console.WriteLine(obj);
              Console.WriteLine(body["room"]);
              // yyyy-mm-ddThh:MM:ss+zz:zz形式を、DateTime型にParseする
              DateTime start = DateTime.Parse(body["startDateTime"], null, System.Globalization.DateTimeStyles.RoundtripKind);
              DateTime end   = DateTime.Parse(body["endDateTime"]  , null, System.Globalization.DateTimeStyles.RoundtripKind);
              var usecase = new ReserveApplication(repository);
              var id = usecase.ReserveMeetingRoom(body["room"],
                                                  start.Year,start.Month,start.Day,start.Hour,start.Minute,
                                                  end.Year,end.Month,end.Day,end.Hour,end.Minute,
                                                  int.Parse(body["reserverOfNumber"]),
                                                  body["reserverId"]);
              Reserve reserve = repository.Find(id);
              return new LambdaResponse {
                  StatusCode = HttpStatusCode.OK,
                  Headers = null,
                  Body = JsonConvert.SerializeObject(
                      new ResponseParam 
                      {
                          Room = reserve.Room.ToString()
                      })
              };
              //return new Response(reserve.Room.ToString(), request);
          }
      }
    }

    public class LambdaResponse
    {
        [JsonProperty(PropertyName = "statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty(PropertyName = "headers")]
        public Dictionary<string, string> Headers { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }
    }

    public class ResponseParam
    {
        [JsonProperty(PropertyName = "room")]
        public string Room { get; set; }
    }

    public class Response
    {
      public string Message {get; set;}
      public Request Request {get; set;}

      public Response(string message, Request request){
        Message = message;
        Request = request;
      }
    }

    

    public class Request
    {
      public string Room {get; set;}
      public string StartDateTime {get; set;}
      public string EndDateTime {get; set;}
      public string ReserverOfNumber { get; set; }
      public string ReserverId { get; set; }
      public Request(string room,
                    string startDateTIme,
                    string endDateTime,
                    string reserverOfNumber,
                    string reserverId)
        {
        Room             = room;
        StartDateTime    = startDateTIme;
        EndDateTime      = endDateTime;
        ReserverOfNumber = reserverOfNumber;
        ReserverId       = reserverId;
      }
    }
}
