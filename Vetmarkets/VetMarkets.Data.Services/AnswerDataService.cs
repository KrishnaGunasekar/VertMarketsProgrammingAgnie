using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using VetMarkets.Data.Services.Contracts;
using VetMarkets.Models.ApiResponse;
using VetMarkets.Programming.Common.Http;

namespace VetMarkets.Data.Services
{
    public class AnswerDataService : IAnswer
    {
        private static HttpClient _vetMarketsHttpClient;
        private readonly string _endPoint;
        
        public AnswerDataService(HttpClient vetMarketsHttpClient,string endPoint)
        {
            _vetMarketsHttpClient = vetMarketsHttpClient ?? throw new InvalidOperationException("Http Client Cannot be null");
            _endPoint = endPoint ?? throw new InvalidOperationException("Endpoint Cannot be null");            
        }       

        public AnswerResponseModel PostAnswer(string requestBody)
        {
            var dataResponse = VetsMarketHttpAction.Post(_vetMarketsHttpClient, _endPoint, requestBody);
            var tokenResponseModel = JsonConvert.DeserializeObject<AnswerResponseModel>(dataResponse);
            return tokenResponseModel;            
        }      
    }
}
