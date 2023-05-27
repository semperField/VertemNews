using VertemNews.Application.Models;

namespace VertemNews.Application.Interfaces
{
    public interface INewsService
    {
        Task<List<NewsModel>> GetNewsAsync();
        Task<NewsModel> GetNewsByIdAsync(Guid id);
        Task<List<NewsModel>> GetNewsByCategoryAsync(string category);
        Task<List<NewsModel>> GetNewsBySourceAsync(string source);
        Task<List<NewsModel>> GetNewsByKeywordAsync(string keyword);
        Task InsertAllNewAsync(NewApiModel news);
    }
}