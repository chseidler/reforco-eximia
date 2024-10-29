using ReforcoEximia.HttpService.Comum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReforcoEximia.HttpService.Dominio.Regras.infra.Mapeamento;

public class RegraConfiguration : IEntityTypeConfiguration<RegraPorConveniada>
{
    public void Configure(EntityTypeBuilder<RegraPorConveniada> builder)
    {
        builder.ToTable("RegrasPorTurma");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome);
        builder
            .Property(p => p.Regra)
            .HasColumnType("varchar(max)")
            .HasConversion(
                c => c.ToNameTypeJson(),
                s => s.ToNameTypeObject<IValidacaoInscricao>())
            .IsRequired();
    }
}