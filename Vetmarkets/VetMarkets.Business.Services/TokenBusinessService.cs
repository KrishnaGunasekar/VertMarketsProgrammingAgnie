using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VetMarkets.Data.Services;
using VetMarkets.Programming.Common.Contracts;
using VetMarkets.Programming.Common.Http;

namespace VetMarkets.Business.Services
{
    public static class TokenBusinessService
    {
        public  static string GetToken(HttpClient VetMarketsHttpClient, string endPoint)
        {
            IToken tokenDataService = new TokenDataService();
            var result = tokenDataService.GetToken(VetMarketsHttpClient, endPoint);
            return result?.Token;
        }
    }
}
