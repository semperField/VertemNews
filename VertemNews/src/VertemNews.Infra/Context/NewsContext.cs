using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VertemNews.Domain.Entities;

namespace VertemNews.Infra.Context
{
    public class NewsContext : DbContext
    {
        private string ConnectionString;

        public NewsContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public DbSet<New> News { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(NewsContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(ConnectionString);
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }
    }
}