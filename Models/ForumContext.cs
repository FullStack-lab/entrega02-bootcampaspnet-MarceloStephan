using System.Data.Entity;

namespace Sistema_de_Comentários_e_Respostas
{
    public class ForumContext : DbContext
    {
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comentario>()
                .HasOptional(c => c.ComentarioPai)  
                .WithMany(c => c.Respostas)        
                .HasForeignKey(c => c.ComentarioPaiId);  

            base.OnModelCreating(modelBuilder);
        }
    }
}
