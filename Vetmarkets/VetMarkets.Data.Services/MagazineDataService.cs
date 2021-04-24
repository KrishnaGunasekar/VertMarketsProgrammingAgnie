using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using VetMarkets.Data.Services.Contracts;
using VetMarkets.Models.ApiResponse;
using VetMarkets.Programming.Common.Http;

namespace VetMarkets.Data.Services
{
    public class MagazineDataService : IMagazine
    {
        private static HttpClient _vetMarketsHttpClient;
        private readonly string _endPoint;
        
        public MagazineDataService(HttpClient vetMarketsHttpClient,string endPoint)
        {
            _vetMarketsHttpClient = vetMarketsHttpClient ?? throw new InvalidOperationException("Http Client Cannot be null");
            _endPoint = endPoint ?? throw new InvalidOperationException("Endpoint Cannot be null");            
        }            

        public MagazinesResponseModel GetMagazines()
        {
            var dataResponse =  VetsMarketHttpAction.Get(_vetMarketsHttpClient, _endPoint);
            var tokenResponseModel = JsonConvert.DeserializeObject<MagazinesResponseModel>(dataResponse);
            return tokenResponseModel;
        }

        
    }
}
