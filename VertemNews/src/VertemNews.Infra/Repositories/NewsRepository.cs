using VertemNews.Domain.Entities;
using VertemNews.Domain.Interfaces.Repositories;
using VertemNews.Infra.Context;
using VertemNews.Infra.Repositories.Base;

namespace VertemNews.Infra.Repositories
{
    public class NewsRepository : BaseRepository<New>, INewsRepository
    {
        public NewsRepository(NewsContext context) : base(context)
        {
        }
    }
}