using BookChangelog.API.Infrastructure.EntityConfigurations;
using BookChangelog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookChangelog.API.Infrastructure;

public class BookChangelogContext : DbContext
{
    public BookChangelogContext(DbContextOptions<BookChangelogContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    
    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Book> Books => Set<Book>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookChangeHistoryConfiguration).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}