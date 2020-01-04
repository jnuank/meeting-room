using Amazon.Lambda.Core;
using System;
using modeling_mtg_room.Domain.Reserve;
using InMemoryInfrastructure;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {
      public LambdaResponse Hello(Request request)
      {
          // yyyy-mm-ddThh:MM:ss+zz:zz形式を、DateTime型にParseする
          // DateTime start = DateTime.Parse(request.StartDateTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
          // DateTime end   = DateTime.Parse(request.EndDateTime  , null, System.Globalization.DateTimeStyles.RoundtripKind);

          DateTime start = DateTime.Parse("2020-1-10T10:00:40+09:00", null, System.Globalization.DateTimeStyles.RoundtripKind);
          DateTime end   = DateTime.Parse("2020-1-10T18:45:40+09:00" , null, System.Globalization.DateTimeStyles.RoundtripKind);

          var repository = new InMemoryReserveRepository();
          // ユースケースを実行する
          var usecase = new ReserveApplication(repository);
          var id = usecase.ReserveMeetingRoom("A",
                                              start.Year,start.Month,start.Day,start.Hour,start.Minute,
                                              end.Year,end.Month,end.Day,end.Hour,end.Minute,
                                              2,
                                              "abc");
          // var id = usecase.ReserveMeetingRoom(request.Room,
          //                                     start.Year,start.Month,start.Day,start.Hour,start.Minute,
          //                                     end.Year,end.Month,end.Day,end.Hour,end.Minute,
          //                                     int.Parse(request.ReserverOfNumber),
          //                                     request.ReserverId);
          Reserve reserve = repository.Find(id);
          return new LambdaResponse {
              StatusCode = HttpStatusCode.OK,
              Headers = null,
              Body = JsonConvert.SerializeObject(
                  new ResponseParam 
                  {
                      Romm = reserve.Room.ToString()
                  })
          };
          //return new Response(reserve.Room.ToString(), request);
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
        public string Romm { get; set; }
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
