using VertemNews.Application.Models;

namespace VertemNews.Application.Interfaces
{
    public interface INewsApiIntegrationService
    {
        Task<NewApiModel> GetNewsApiAsync();
    }
}