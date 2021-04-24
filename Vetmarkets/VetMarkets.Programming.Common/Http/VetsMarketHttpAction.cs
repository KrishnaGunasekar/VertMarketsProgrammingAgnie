using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VetMarkets.Programming.Common.Http
{
    public static class VetsMarketHttpAction
    {
        public static string Get(HttpClient httpClient, string endPoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            var response = httpClient.SendAsync(request).Result;
            var dataResponse = response?.Content?.ReadAsStringAsync().Result;
            return dataResponse;
        }

        public static string Post(HttpClient httpClient, string endPoint,string requestBody)
        {
            var data = new StringContent(requestBody, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(endPoint,data).Result;
            var dataResponse = response?.Content?.ReadAsStringAsync().Result;
            return dataResponse;
        }
    }
}
