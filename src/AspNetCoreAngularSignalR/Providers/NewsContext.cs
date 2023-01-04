using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngularSignalR.Providers;

public class NewsContext : DbContext
{
    public NewsContext(DbContextOptions<NewsContext> options) :base(options){ }

    public DbSet<NewsItemEntity> NewsItemEntities => Set<NewsItemEntity>();

    public DbSet<NewsGroup> NewsGroups => Set<NewsGroup>();
}