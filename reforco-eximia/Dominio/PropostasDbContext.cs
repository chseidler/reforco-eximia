using ReforcoEximia.HttpService.Dominio.Inscricoes;
using ReforcoEximia.HttpService.Dominio.Inscricoes.Infra.Mapeamento;
using ReforcoEximia.HttpService.Dominio.Regras;
using ReforcoEximia.HttpService.Dominio.Regras.infra.Mapeamento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ReforcoEximia.HttpService.Dominio;

public class PropostasDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "propostas";

    public PropostasDbContext(DbContextOptions<PropostasDbContext> options) : base(options) { }

    public DbSet<Proposta> Propostas { get; set; }
    public DbSet<Agente> Agentes { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Conveniada> Conveniadas { get; set; }
    public DbSet<RegraPorConveniada> RegrasPorTurma { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if ((item.State == EntityState.Modified || item.State == EntityState.Added)
                    && item.Properties.Any(c => c.Metadata.Name == "DataUltimaAlteracao"))
                    item.Property("DataUltimaAlteracao").CurrentValue = DateTime.UtcNow;

                if (item.State == EntityState.Added)
                    if (item.Properties.Any(c => c.Metadata.Name == "DataCadastro") && item.Property("DataCadastro").CurrentValue.GetType() != typeof(DateTime))
                        item.Property("DataCadastro").CurrentValue = DateTime.UtcNow;
            }
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }
        catch (DbUpdateException e)
        {
            throw new Exception();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PropostaConfiguration());
        modelBuilder.ApplyConfiguration(new AgenteConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new RegraConfiguration());
    }
}

public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<PropostasDbContext>
{
    public PropostasDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PropostasDbContext>();
        var connectionString = "Server=localhost;Database=PropostasDB;User Id=sa;Password=SenhaForte123!;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });

        return new PropostasDbContext(optionsBuilder.Options);
    }
}
