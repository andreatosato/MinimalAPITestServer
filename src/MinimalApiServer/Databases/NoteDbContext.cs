using Microsoft.EntityFrameworkCore;

namespace MinimalApiServer.Databases;

public class NotesDbContext : DbContext
{
    public NotesDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Note>? Notes { get; set; }
}

public class Note
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedOn { get; set; }
}
