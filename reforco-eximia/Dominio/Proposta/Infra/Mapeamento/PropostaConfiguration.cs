using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReforcoEximia.HttpService.Dominio.Inscricoes.Infra.Mapeamento;

public class PropostaConfiguration : IEntityTypeConfiguration<Proposta>
{
    public void Configure(EntityTypeBuilder<Proposta> builder)
    {
        builder.ToTable("Propostas");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.ClienteCpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(i => i.AgenteId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Ativa)
            .IsRequired();
    }
}
