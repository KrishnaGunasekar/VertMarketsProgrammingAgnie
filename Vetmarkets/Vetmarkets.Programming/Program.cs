using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Microsoft.Extensions.Configuration;
using Vetmarkets.Programming.Logger;
using VetMarkets.Orchestrator.Services;

namespace Vetmarkets.Programming
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot configuration = GetAppConfiguration();
            var configurationData = configuration.AsEnumerable().ToList();
            var logFileDirectoryPath = configurationData.FirstOrDefault(a => a.Key == "logFileDirectoryPath").Value;

            var directoryPath = logFileDirectoryPath;
            var logWriter = VetMarketsLogger.GetLoggerInstance
                (string.Concat("VetMarkets", "-", DateTime.Now.ToString("yyyy-MM-dd")), directoryPath);

            try
            {

                var magazineSubscribersOrchestratorService = new MagazineSubscribersOrchestratorService(configurationData, logWriter);
                var answerResponse = magazineSubscribersOrchestratorService.SubmitSubscribersInAllCategories();

                logWriter.Info($"Token :{ answerResponse.Token}");
                logWriter.Info($"Success :{ answerResponse.Success}");
                logWriter.Info($"Is Answer Correct :{ answerResponse.Answer?.AnswerCorrect}");
                logWriter.Info($"Total Time :{ answerResponse.Answer?.TotalTime}");
                logWriter.Info($"Should be :{ answerResponse.Answer?.ShouldBe}");

                Console.WriteLine($"Token :{ answerResponse.Token}");
                Console.WriteLine($"Success :{ answerResponse.Success}");

                Console.WriteLine($"Is Answer Correct :{ answerResponse.Answer?.AnswerCorrect}");
                Console.WriteLine($"Total Time :{ answerResponse.Answer?.TotalTime}");
                Console.WriteLine($"Should be :{ answerResponse.Answer?.ShouldBe}");

            }
            catch (Exception ex)
            {
                logWriter.Error(string.Concat("An error has occured in the vet markets application - ", ex.Message, ex.InnerException?.Message, ex.StackTrace));
            }



            Console.WriteLine("Press Any Key to Exit...");
            Console.ReadKey();
        }


        private static IConfigurationRoot GetAppConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            return configuration;
        }
    }
}
