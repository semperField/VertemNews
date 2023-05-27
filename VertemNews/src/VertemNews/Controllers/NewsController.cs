using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VertemNews.Application.Interfaces;
using VertemNews.Application.Models;
using VertemNews.Extensions;

namespace VertemNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {   
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerResponse(200, Type = typeof(List<NewsModel>))]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetNewsAsync();
            return result.ToSuccessRequest();
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, Type = typeof(NewsModel))]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetNewsByIdAsync(id);
            return result.ToSuccessRequest();
        }

        [HttpGet("category/category")]
        [SwaggerResponse(200, Type = typeof(List<NewsModel>))]
        public async Task<IActionResult> GetCategory(string category)
        {
            var result = await _service.GetNewsByCategoryAsync(category);
            return result.ToSuccessRequest();
        }

        [HttpGet("source/{source}")]
        [SwaggerResponse(200, Type = typeof(List<NewsModel>))]
        public async Task<IActionResult> GetSource(string source)
        {
            var result = await _service.GetNewsBySourceAsync(source);
            return result.ToSuccessRequest();
        }

        [HttpGet("search/{keyword}")]
        [SwaggerResponse(200, Type = typeof(List<NewsModel>))]
        public async Task<IActionResult> GetSearch(string keyword)
        {
            var result = await _service.GetNewsByKeywordAsync(keyword);
            return result.ToSuccessRequest();
        }
    }
}