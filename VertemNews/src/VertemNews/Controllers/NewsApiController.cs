using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VertemNews.Application.Interfaces;
using VertemNews.Application.Models;
using VertemNews.Extensions;

namespace VertemNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsApiController : ControllerBase
    {
        private readonly INewsService _service;
        private readonly INewsApiIntegrationService _integrationService;
        public NewsApiController(INewsService service, INewsApiIntegrationService integrationService)
        {
            _service = service;
            _integrationService = integrationService;
        }

        [HttpPost("InitialCharge")]
        [SwaggerResponse(200, Type = typeof(List<NewApiModel>))]
        public async Task<IActionResult> PostAsync()
        {
            var result = await _service.GetNewsAsync();
            if (result != null && result.Count > 0)
            {
                return ResultExtension.ToSuccessRequest();
            }

            var newsApiModel = await _integrationService.GetNewsApiAsync();
            await _service.InsertAllNewAsync(newsApiModel);
            return ResultExtension.ToSuccessRequest();
        }
    }
}