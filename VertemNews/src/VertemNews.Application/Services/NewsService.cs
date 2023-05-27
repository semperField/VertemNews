using VertemNews.Application.Helpers;
using VertemNews.Application.Interfaces;
using VertemNews.Application.Mappers;
using VertemNews.Application.Models;
using VertemNews.Domain.Entities;
using VertemNews.Domain.Interfaces.Repositories;

namespace VertemNews.Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;

        public NewsService(INewsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<NewsModel>> GetNewsAsync()
        {
            try
            {
                var filter = PredicateBuilder.True<New>();
                filter = null;

                var result = await _repository.FindAsync(filter, null, o => o.OrderByDescending(x => x.PublishedAt));
                
                return result.MapToNewsListModel();
            }
            catch (Exception ex)
            {                
                return null;
            }
        }

        public async Task<NewsModel> GetNewsByIdAsync(Guid id)
        {
            try
            {
                var filter = PredicateBuilder.True<New>();
                filter = null;

                var result = await _repository.FindByIdAsync(id);

                return result.MapToNewsModel();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<NewsModel>> GetNewsByTitleAsync(string title)
        {
            try
            {
                var filter = PredicateBuilder.True<New>();
                filter = filter.And(x => x.Title.Contains(title));
                
                var result = await _repository.FindAsync(filter, null, o => o.OrderByDescending(x => x.PublishedAt));

                return result.MapToNewsListModel();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<NewsModel>> GetNewsBySourceAsync(string source)
        {
            try
            {
                var filter = PredicateBuilder.True<New>();
                filter = filter.And(x => x.Source.Contains(source));

                var result = await _repository.FindAsync(filter, null, o => o.OrderByDescending(x => x.PublishedAt));

                return result.MapToNewsListModel();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<NewsModel>> GetNewsByKeywordAsync(string keyword)
        {
            try
            {
                var filter = PredicateBuilder.True<New>();
                filter = filter.And(x => x.Content.Contains(keyword));

                var result = await _repository.FindAsync(filter, null, o => o.OrderByDescending(x => x.PublishedAt));

                return result.MapToNewsListModel();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task InsertAllNewAsync(NewApiModel newApiModel)
        {
            try
            {
                var entities = newApiModel.MapToNewsApiList();
                await _repository.InsertAllAsync(entities);
                await _repository.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
    }
}