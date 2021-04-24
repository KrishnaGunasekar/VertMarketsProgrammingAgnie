using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace VetMarkets.Programming.Common.Http
{
    public static class VetMarketsHttpClient
    {
        public static HttpClient GetInstance()
        {
            var vetMarketsRestClient = new HttpClient();
            vetMarketsRestClient.DefaultRequestHeaders.Accept.Clear();            
            vetMarketsRestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return vetMarketsRestClient;
        }
    }
}
