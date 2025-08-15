using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFlix.Dados.Entidades;

namespace MyFlix.Dados.Map
{
    public class FilmesConfiguration : IEntityTypeConfiguration<Filmes>
    {
        public void Configure(EntityTypeBuilder<Filmes> builder)
        {
            builder.ToTable(nameof(Filmes));
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                   .HasColumnName("Nome")
                   .HasColumnType("VARCHAR(100)")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(f => f.DataCadastro)
                  .HasColumnName("DataCadastro")
                  .HasColumnType("DATETIME")
                  .IsRequired();

            builder.Property(f => f.AnoLancamento)
                  .HasColumnName("AnoLancamento")
                  .HasColumnType("DATETIME")
                  .IsRequired();

            builder.Property(f => f.Genero)
                  .HasColumnName("Genero")
                  .HasColumnType("VARCHAR(50)")
                  .IsRequired();

            builder.Property(f => f.Titulo)
                 .HasColumnName("Titulo")
                 .HasColumnType("VARCHAR(100)")
                 .IsRequired();

            builder.Property(f => f.AvaliacaoId)
                .HasColumnName("AvaliacaoId")
                .HasColumnType("INT")
                .IsRequired();


            builder.HasOne(f => f.Avaliacao)
                 .WithMany()
                 .HasForeignKey(f => f.AvaliacaoId)
                 .OnDelete(DeleteBehavior.Cascade);
  
        }
    }
}
