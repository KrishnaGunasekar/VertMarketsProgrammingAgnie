using System;
using System.Threading.Tasks;
using VetMarkets.Models.ApiResponse;

namespace VetMarkets.Business.Services.Contracts
{
    public interface IAnswer
    {
        public AnswerResponseModel PostAnswer(string requestBody);
    }
}
