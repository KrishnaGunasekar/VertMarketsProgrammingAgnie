using System;
using System.Net.Http;
using System.Threading.Tasks;
using VetMarkets.Business.Services.Contracts;
using VetMarkets.Data.Services;
using VetMarkets.Models.ApiResponse;

namespace VetMarkets.Business.Services
{
    public class MagazineBusinessService : IMagazine
    {
        private VetMarkets.Data.Services.Contracts.IMagazine _magazineDataService;
        

        public MagazineBusinessService(HttpClient VetMarketsHttpClient, string endPoint)
        {
            _magazineDataService = new MagazineDataService(VetMarketsHttpClient, endPoint);
        }
        public MagazinesResponseModel GetMagazines()
        {
            var magazines =  _magazineDataService.GetMagazines();
            return magazines;
        }
    }
}




