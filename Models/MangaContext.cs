using Microsoft.EntityFrameworkCore;

namespace IsaacWebAPI.Models;

public class MangaContext : DbContext
{
    public MangaContext(DbContextOptions<MangaContext> options)
        : base(options)
    {
    }

    public DbSet<Manga> Manga { get; set; } = null!;
}