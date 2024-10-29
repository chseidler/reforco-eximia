using CSharpFunctionalExtensions;
using ReforcoEximia.HttpService.Dominio.Regras;

namespace ReforcoEximia.HttpService.Dominio.Inscricoes;

public sealed class Proposta : Entity<Guid>
{
    private Proposta()
    {
    }

    private Proposta(Guid id, string clienteCpf, Guid agenteId, Conveniada conveniada, bool ativa)
    {
        Id = id;
        ClienteCpf = clienteCpf;
        AgenteId = agenteId;
        Conveniada = conveniada;
        Ativa = ativa;
    }

    public Guid Id { get; }
    public string ClienteCpf { get; }
    public Guid AgenteId { get; }
    public Conveniada Conveniada { get; }
    public bool Ativa { get; }

    public static Result<Proposta> Criar(Cliente cliente, Conveniada conveniada, Guid agenteId, int quantidadeParcelas, decimal valorSolicitado, IEnumerable<IValidacaoInscricao> regras)
    {
        foreach (var regra in regras)
        {
            var resultado = regra.Validar(cliente, conveniada, quantidadeParcelas, valorSolicitado);
            if (resultado.IsFailure)
                return Result.Failure<Proposta>(resultado.Error);
        }

        var inscricao = new Proposta(Guid.NewGuid(), cliente.Cpf, agenteId, conveniada, true);
        return Result.Success(inscricao);
    }
}
