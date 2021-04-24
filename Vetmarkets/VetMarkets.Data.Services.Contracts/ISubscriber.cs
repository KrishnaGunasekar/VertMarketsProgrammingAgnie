using System;
using System.Threading.Tasks;
using VetMarkets.Models.ApiResponse;

namespace VetMarkets.Data.Services.Contracts
{
    public interface ISubscriber
    {
        public SubscribersResponseModel GetSubscribers();
    }
}
