using Microsoft.EntityFrameworkCore;
using MyFlix.Dados.Entidades;
using MyFlix.Dados.Map;

namespace MyFlix.Dados.Cofiguracao
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Avaliacao> Avaliacao { get; set; }
        public DbSet<Filmes> Filmes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AvaliacaoConfiguration());
            modelBuilder.ApplyConfiguration(new FilmesConfiguration());

        }
  
    }
}
