using Amazon.Lambda.Core;
using System;
using modeling_mtg_room.Domain.Reserve;
using InMemoryInfrastructure;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {
      public Response Hello(Request request)
      {
          DateTime start = DateTime.Parse(request.StartDateTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
          DateTime end   = DateTime.Parse(request.EndDateTime  , null, System.Globalization.DateTimeStyles.RoundtripKind);

          var repository = new InMemoryReserveRepository();
          // ユースケースを実行する
          var usecase = new ReserveApplication(repository);
          var id = usecase.ReserveMeetingRoom(request.Room,
                                              start.Year,start.Month,start.Day,start.Hour,start.Minute,
                                              end.Year,end.Month,end.Day,end.Hour,end.Minute,
                                              int.Parse(request.ReserverOfNumber),
                                              request.ReserverId);
          Reserve reserve = repository.Find(id);
          return new Response(reserve.Room.ToString(), request);
      }
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
