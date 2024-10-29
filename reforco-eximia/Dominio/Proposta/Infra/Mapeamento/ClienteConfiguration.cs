using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReforcoEximia.HttpService.Dominio.Inscricoes.Infra.Mapeamento;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(a => a.Cpf);

        builder.Property(a => a.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(a => a.Idade)
            .IsRequired();
    }
}
