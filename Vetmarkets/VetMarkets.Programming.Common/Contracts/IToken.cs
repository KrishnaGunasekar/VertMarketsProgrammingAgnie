using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VetMarkets.Models.ApiResponse;

namespace VetMarkets.Programming.Common.Contracts
{
    public interface IToken
    {
        TokenResponseModel GetToken(HttpClient httpClient, string endPoint);
    }
}
