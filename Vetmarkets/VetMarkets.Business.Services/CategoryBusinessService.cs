using System;
using System.Net.Http;
using System.Threading.Tasks;
using VetMarkets.Business.Services.Contracts;
using VetMarkets.Data.Services;
using VetMarkets.Models.ApiResponse;

namespace VetMarkets.Business.Services
{
    public class CategoryBusinessService : ICategory
    {
        private VetMarkets.Data.Services.Contracts.ICategory _categoryDataService;
        

        public CategoryBusinessService(HttpClient VetMarketsHttpClient, string endPoint)
        {
            _categoryDataService = new CategoriesDataService(VetMarketsHttpClient, endPoint);
        }
        public CategoryResponseModel GetCategories()
        {
            var categories =  _categoryDataService.GetCategories();
            return categories;
        }
    }
}




