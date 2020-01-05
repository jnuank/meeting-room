using Amazon.Lambda.Core;
using System;
using S3Infrastructure;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Codeplex.Data;
using modeling_mtg_room.Domain.Application;
using modeling_mtg_room.Domain.Repository;
using modeling_mtg_room.Domain.Application.Models;


[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class Handler
    {
        private readonly IReserveRepository repository;
        private readonly ReserveApplication usecase;
        public Handler()
        {
            repository = new S3ReserveRepository();
            usecase = new ReserveApplication(repository);
        }
        public async Task<LambdaResponse> Hello(Stream input, ILambdaContext context)
        {
            var reader = new StreamReader(input);
            var val = await reader.ReadToEndAsync();

            // Inputで来たJSONをオブジェクトにパースする
            var obj = DynamicJson.Parse(val);
            var body = DynamicJson.Parse(obj["body"]);

            // yyyy-mm-ddThh:MM:ss+zz:zz形式を、DateTime型にParseする
            DateTime start = DateTime.Parse(body["startDateTime"], null, System.Globalization.DateTimeStyles.RoundtripKind);
            DateTime end   = DateTime.Parse(body["endDateTime"]  , null, System.Globalization.DateTimeStyles.RoundtripKind);

            try {
                string id = await usecase.ReserveMeetingRoomAsync(body["room"],
                                                    start.Year,start.Month,start.Day,start.Hour,start.Minute,
                                                    end.Year,end.Month,end.Day,end.Hour,end.Minute,
                                                    int.Parse(body["reserverOfNumber"]),
                                                    body["reserverId"]);

                return new LambdaResponse {
                    StatusCode = HttpStatusCode.OK,
                    Headers = null,
                    Body = JsonConvert.SerializeObject(
                        new ResponseParam 
                        {
                            ReserveId = id
                        }
                    )
                };
            }catch(Exception ex){
                return new LambdaResponse {
                    StatusCode = HttpStatusCode.OK,
                    Headers = null,
                    Body = JsonConvert.SerializeObject(
                        new ErrorResponse {
                            ErrorMessage = ex.Message,
                            ErrorType = ex.GetType().ToString(),
                            StackTrace = ex.StackTrace
                        }
                    )
                };
            }
        }

        public async Task<LambdaResponse> GetReserve(Stream input, ILambdaContext context)
        {
            var reader  = new StreamReader(input);
            string val  = await reader.ReadToEndAsync();
            dynamic obj = DynamicJson.Parse(val);
            string id   = obj["pathParameters"]["reserveId"];

            ReserveModel reserve = await usecase.FindReserveAsync(id);

            return new LambdaResponse {
                StatusCode = HttpStatusCode.OK,
                Headers = null,
                Body = JsonConvert.SerializeObject(
                    new ResponseParam 
                    {
                        ReserveId = reserve.Id,
                        Room = reserve.Room
                    }
                )
            }; 
        }
        public async Task<LambdaResponse> DeleteReserve(Stream input, ILambdaContext context)
        {
            var reader  = new StreamReader(input);
            string val  = await reader.ReadToEndAsync();
            dynamic obj = DynamicJson.Parse(val);
            string id   = obj["pathParameters"]["reserveId"];

            await usecase.DeleteReserveAsync(id);
            return new LambdaResponse {
                StatusCode = HttpStatusCode.OK,
                Headers = null,
                Body = JsonConvert.SerializeObject(
                    new ResponseParam()
                )
            }; 
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
        [JsonProperty(PropertyName = "reserveId")]
        public string ReserveId { get; set; }

        [JsonProperty(PropertyName = "room")]
        public string Room { get; set; }

    }

    public class ErrorResponse
    {
        [JsonProperty(PropertyName = "errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty(PropertyName = "errorType")]
        public string ErrorType { get; set; }

        [JsonProperty(PropertyName = "stackTrace")]
        public string StackTrace { get; set; }
    }
}
