using VertemNews.Application.Models;
using VertemNews.Domain.Entities;

namespace VertemNews.Application.Mappers
{
    public static class NewsMapper
    {
        public static List<New> MapToNewsList(this List<NewsModel> model)
        {
            return model.Select(model => model.MapToNewsEntity()).ToList();
        }        

        public static New MapToNewsEntity(this NewsModel model)
        {
            return new New
            {
                Id = model.Id,
                Author = model.Author,
                Content = model.Content,
                PublishedAt = model.PublishedAt,
                Source = model.Source,
                Title = model.Title,
                Url = model.Url,
                UrlImage = model.UrlImage,
                Description = model.Description ?? " "
            };
        }

        public static List<New> MapToNewsApiList(this NewApiModel model)
        {
            return model.Articles.Select(model => model.MapToNewsApiEntity()).ToList();
        }
        public static New MapToNewsApiEntity(this Articles model)
        {
            return new New
            {
                Id = Guid.NewGuid(),
                Author = model.Author ?? " ",
                Content = model.Content ?? " ",
                PublishedAt = model.PublishedAt,
                Source = model.Source.Name ?? " ",
                Title = model.Title ?? " ",
                Url = model.Url ?? " ",
                UrlImage = model.UrlImage ?? " ",
                Description = model.Description ?? " "
            };
        }

        public static List<NewsModel> MapToNewsListModel(this List<New> model)
        {
            return model.Select(model => model.MapToNewsModel()).ToList();
        }

        public static NewsModel MapToNewsModel(this New entity)
        {
            return new NewsModel
            {
                Id = entity.Id,
                Author = entity.Author,
                Content = entity.Content,
                PublishedAt = entity.PublishedAt,
                Source = entity.Source,
                Title = entity.Title,
                Url = entity.Url,
                UrlImage = entity.UrlImage,
                Description = entity.Description ?? " "
            };
        }
    }
}