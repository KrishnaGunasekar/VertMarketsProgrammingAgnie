using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using VetMarkets.Models.ApiResponse;
using VetMarkets.Programming.Common.Contracts;
using VetMarkets.Programming.Common.Http;

namespace VetMarkets.Data.Services
{
    public class TokenDataService : IToken
    {
        public  TokenResponseModel GetToken(HttpClient httpClient, string endPoint)
        {
            var dataResponse =  VetsMarketHttpAction.Get(httpClient, endPoint);
            var tokenResponseModel = JsonConvert.DeserializeObject<TokenResponseModel>(dataResponse);
            return tokenResponseModel;
        }
    }
}
