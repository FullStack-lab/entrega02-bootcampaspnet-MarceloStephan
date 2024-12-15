using System.Data.Entity;

public class ForumContext : DbContext
{
    public ForumContext() : base("ForumDB") { }

    public DbSet<Comentario> Comentarios { get; set; }
}
