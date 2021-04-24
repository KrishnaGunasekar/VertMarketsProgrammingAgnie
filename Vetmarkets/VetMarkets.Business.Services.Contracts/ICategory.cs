using System;
using System.Threading.Tasks;
using VetMarkets.Models.ApiResponse;

namespace VetMarkets.Business.Services.Contracts
{
    public interface ICategory
    {
        public CategoryResponseModel GetCategories();
    }
}
