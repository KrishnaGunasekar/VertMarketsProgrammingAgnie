using System.Collections.Generic;
using VetMarkets.Business.Services;
using System.Linq;
using System.Threading.Tasks;
using VetMarkets.Business.Services.Contracts;
using VetMarkets.Programming.Common.Http;
using VetMarkets.Models.Business;
using Newtonsoft.Json;
using System;
using VetMarkets.Models.ApiResponse;
using log4net;

namespace VetMarkets.Orchestrator.Services
{
    public class MagazineSubscribersOrchestratorService
    {
        private readonly List<KeyValuePair<string, string>> configurationData;
        private ILog _log;
        public MagazineSubscribersOrchestratorService(List<KeyValuePair<string, string>> configurationData,ILog log)
        {
            this.configurationData = configurationData;
            _log = log;
        }

        public AnswerResponseModel SubmitSubscribersInAllCategories()
        {

            var httpClient = VetMarketsHttpClient.GetInstance();

            #region Get Token
            var apiBaseUrl = this.configurationData.FirstOrDefault(a => a.Key == "ApiBaseUrl").Value;
            var tokenEndPoint = string.Concat(apiBaseUrl, "/api/token");
            var token = TokenBusinessService.GetToken(httpClient, tokenEndPoint);

            _log.Debug($"Retrieved Token {token}");
            #endregion

            #region Get Categories

            var categoriesEndPoint = string.Concat(apiBaseUrl, "/api/categories/", token);
            ICategory categoryBusinessService = new CategoryBusinessService(httpClient, categoriesEndPoint);
            var categories =  categoryBusinessService.GetCategories();
            _log.Debug($"Completed retrieving categories");
            #endregion

            #region Get Subscribers

            var subscribersEndPoint = string.Concat(apiBaseUrl, "/api/subscribers/", token);
            ISubscriber subscriberBusinessService = new SubscriberBusinessService(httpClient, subscribersEndPoint);
            var subscribers =  subscriberBusinessService.GetSubscribers(); 

            var subscriberMagazines = new List<SubscriberMagazines>();
            foreach (var record in subscribers.Subscribers)
            {
                var magazineIds = record.MagazineIds;

                foreach (var magazineId in magazineIds)
                {
                    subscriberMagazines.Add(new SubscriberMagazines { Id = record.Id, MagazineId = magazineId });
                }
            }

            _log.Debug($"Completed retrieving subscribers");

            #endregion

            #region Get Category Magazines

            var categoryMagazineEndBasePoint = string.Concat(apiBaseUrl, "/api/magazines/", token);
            IMagazine magazineBusinessService;
            List<CategoryMagazines> categoryMagazines = new List<CategoryMagazines>();

            foreach (var category in categories.Categories)
            {
                var requestEndPoint = string.Concat(categoryMagazineEndBasePoint, "/", category);
                magazineBusinessService = new MagazineBusinessService(httpClient, requestEndPoint);
                var magazines =  magazineBusinessService.GetMagazines();
                foreach (var magazine in magazines.Magazines)
                {
                    categoryMagazines.Add(new CategoryMagazines { Category = category, MagazineId = magazine.Id });
                }
            }


            var SubscribersInAllCategories = (from u in (from s in subscriberMagazines.ToList()
                                              join mg in categoryMagazines.ToList() on s.MagazineId equals mg.MagazineId
                                              select new { s.Id, mg.Category, mg.MagazineId }).ToList().ToLookup(a => new { a.Id })
                                   .Select(a => new
                                   {
                                       a.Key.Id,
                                       CategoriesCount = a.Select(x => x.Category).Distinct().Count()
                                   }).ToList().Where(a => a.CategoriesCount == categories.Categories.Count()).ToList()
                                   join su in subscribers.Subscribers on u.Id equals su.Id
                                   select new { su.Id, su.FirstName, su.LastName }).Distinct().ToList();



            _log.Debug($"Completed retrieving category subscribers");
            #endregion

            #region Submitting the Answer
            var answerRequestModel = new AnswerRequestModel();
            answerRequestModel.Subscribers.AddRange(SubscribersInAllCategories.Select(a => a.Id).Distinct().ToList());
            var answerRequest = JsonConvert.SerializeObject(answerRequestModel);
            var postAnswerEndPoint = string.Concat(apiBaseUrl, "/api/answer/", token);

            _log.Debug($"Following were the subscribers who subscribed in all categories");

            foreach (var record in answerRequestModel.Subscribers)
            {
                _log.Debug($"Id : {record}");
            }

            var answerBusinessService = new AnswerBusinessService(httpClient, postAnswerEndPoint);
            var answerResponse= answerBusinessService.PostAnswer(answerRequest);

            return answerResponse;

            #endregion


        }
    }
}
