using Newtonsoft.Json;
using System.Net;
using VertemNews.Application.Interfaces;
using VertemNews.Application.Models;

namespace VertemNews.Application.Services
{
    public class NewsApiIntegrationService : INewsApiIntegrationService
    {
        private HttpClient HttpClient;

        public NewsApiIntegrationService()
        {

            HttpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            HttpClient.DefaultRequestHeaders.Add("user-agent", "News-API-csharp/0.1");
            HttpClient.DefaultRequestHeaders.Add("x-api-key", "d8a1cff5abef4e58bf89fae83f90a30a");
        }

        public async Task<NewApiModel> GetNewsApiAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var json = string.Empty;
                    var url = "https://newsapi.org/v2/everything?" +
                              "q=Apple&" +
                              "from=2023-05-26&" +
                              "sortBy=popularity&" +
                              "apiKey=d8a1cff5abef4e58bf89fae83f90a30a";

                    json = await HttpClient.GetStringAsync(url);

                    return JsonConvert.DeserializeObject<NewApiModel>(json);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    return null;
                }
            }
        }
    }
}