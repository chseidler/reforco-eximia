using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReforcoEximia.HttpService.Dominio.Inscricoes.Infra.Mapeamento;

public class AgenteConfiguration : IEntityTypeConfiguration<Agente>
{
    public void Configure(EntityTypeBuilder<Agente> builder)
    {
        builder.ToTable("Agentes");

        builder.HasKey(t => t.Id);
    }
}
