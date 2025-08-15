using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFlix.Dados.Entidades;

namespace MyFlix.Dados.Map
{
    public class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.ToTable(nameof(Avaliacao));
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nota)
                  .HasColumnName("Nota")
                  .HasColumnType("INT")
                  .IsRequired();


            builder.Property(a => a.Assistido)
                 .HasColumnName("Assistido")
                 .HasColumnType("BIT")
                 .IsRequired();



        }
    }
}
