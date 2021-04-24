using System;
using System.Net.Http;
using System.Threading.Tasks;
using VetMarkets.Business.Services.Contracts;
using VetMarkets.Data.Services;
using VetMarkets.Models.ApiResponse;

namespace VetMarkets.Business.Services
{
    public class SubscriberBusinessService : ISubscriber
    {
        private VetMarkets.Data.Services.Contracts.ISubscriber _subscriberDataService;       

        public SubscriberBusinessService(HttpClient VetMarketsHttpClient, string endPoint)
        {
            _subscriberDataService = new SubscriberDataService(VetMarketsHttpClient, endPoint);
        }
        public SubscribersResponseModel GetSubscribers()
        {
            var subscribers =  _subscriberDataService.GetSubscribers();
            return subscribers;
        }
      
    }
}




