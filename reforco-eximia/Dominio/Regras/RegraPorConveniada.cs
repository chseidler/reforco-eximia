using CSharpFunctionalExtensions;

namespace ReforcoEximia.HttpService.Dominio.Regras;

public class RegraPorConveniada : Entity<Guid>
{
    // Construtor privado sem parâmetros para o EF
    private RegraPorConveniada() { }

    public RegraPorConveniada(Guid id, string nome, IValidacaoInscricao regra)
    {
        Id = id;
        Nome = nome;
        Regra = regra;
    }

    public string Nome { get; }
    public IValidacaoInscricao Regra { get; }
}
