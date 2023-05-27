using Microsoft.AspNetCore.Mvc;
using VertemNews.Application.Interfaces;
using VertemNews.Extensions;
using System.Net;
using Newtonsoft.Json;
using VertemNews.Application.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace VertemNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsApiController : ControllerBase
    {
        private readonly INewsService _service;
        public NewsApiController(INewsService service)
        {
            _service = service;
        }

        [HttpPost("InitialCharge")]
        [SwaggerResponse(200, Type = typeof(List<NewApiModel>))]
        public async Task<IActionResult> PostAsync()
        {
            var url = "https://newsapi.org/v2/everything?" +
          "q=Apple&" +
          "from=2023-05-26&" +
          "sortBy=popularity&" +
          "apiKey=d8a1cff5abef4e58bf89fae83f90a30a";

            var json = new WebClient().DownloadString(url);

            var newsApiModel = JsonConvert.DeserializeObject<NewApiModel>(json);

            var result = await _service.GetNewsAsync();
            if (result != null && result.Count > 0)
            {
                return ResultExtension.ToSuccessRequest();
            }

            await _service.InsertAllNewAsync(newsApiModel);
            return ResultExtension.ToSuccessRequest();
        }
    }
}