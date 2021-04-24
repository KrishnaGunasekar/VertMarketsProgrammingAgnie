using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using VetMarkets.Data.Services.Contracts;
using VetMarkets.Models.ApiResponse;
using VetMarkets.Programming.Common.Http;

namespace VetMarkets.Data.Services
{
    public class SubscriberDataService : ISubscriber
    {
        private static HttpClient _vetMarketsHttpClient;
        private readonly string _endPoint;
        
        public SubscriberDataService(HttpClient vetMarketsHttpClient,string endPoint)
        {
            _vetMarketsHttpClient = vetMarketsHttpClient ?? throw new InvalidOperationException("Http Client Cannot be null");
            _endPoint = endPoint ?? throw new InvalidOperationException("Endpoint Cannot be null");            
        }       

        public  SubscribersResponseModel GetSubscribers()
        {
            var dataResponse =  VetsMarketHttpAction.Get(_vetMarketsHttpClient, _endPoint);
            var tokenResponseModel = JsonConvert.DeserializeObject<SubscribersResponseModel>(dataResponse);
            return tokenResponseModel;            
        }
    }
}
