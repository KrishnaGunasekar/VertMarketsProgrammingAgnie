using System;
using System.Net.Http;
using System.Threading.Tasks;
using VetMarkets.Business.Services.Contracts;
using VetMarkets.Data.Services;
using VetMarkets.Models.ApiResponse;

namespace VetMarkets.Business.Services
{
    public class AnswerBusinessService : IAnswer
    {
        private VetMarkets.Data.Services.Contracts.IAnswer _answerDataService;        

        public AnswerBusinessService(HttpClient VetMarketsHttpClient, string endPoint)
        {
            _answerDataService = new AnswerDataService(VetMarketsHttpClient, endPoint);
        }
        public AnswerResponseModel PostAnswer(string requestBody)
        {
            var categories =  _answerDataService.PostAnswer(requestBody);
            return categories;
        }
    }
}




