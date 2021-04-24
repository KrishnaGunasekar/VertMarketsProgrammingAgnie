using System;
using System.Threading.Tasks;
using VetMarkets.Models.ApiResponse;

namespace VetMarkets.Data.Services.Contracts
{
    public interface IMagazine
    {
        public MagazinesResponseModel GetMagazines();
    }
}
