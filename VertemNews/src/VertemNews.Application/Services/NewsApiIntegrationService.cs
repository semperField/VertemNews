using Newtonsoft.Json;
using System.Net;
using VertemNews.Application.Interfaces;
using VertemNews.Application.Models;

namespace VertemNews.Application.Services
{
    public class NewsApiIntegrationService : INewsApiIntegrationService
    {
        public async Task<NewApiModel> GetNewsApiAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                var json = string.Empty;
                var url = "https://newsapi.org/v2/everything?" +
                          "q=Apple&" +
                          "from=2023-05-26&" +
                          "sortBy=popularity&" +
                          "apiKey=d8a1cff5abef4e58bf89fae83f90a30a";

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        json = await client.GetStringAsync(url);
                        break;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                if (json == null) return null;

                return JsonConvert.DeserializeObject<NewApiModel>(json);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}