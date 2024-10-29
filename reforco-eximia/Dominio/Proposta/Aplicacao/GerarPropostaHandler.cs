using CSharpFunctionalExtensions;
using ReforcoEximia.HttpService.Dominio.Inscricoes.Infra;
using ReforcoEximia.HttpService.Dominio.Regras.infra;

namespace ReforcoEximia.HttpService.Dominio.Inscricoes.Aplicacao;

public class GerarPropostaHandler(
    PropostasRepositorio inscricoesRepositorio,
    RegraPorConveniadaRepository regraPorConveniadaRepository)
{
    public async Task<Result<Proposta>> Handle(GerarPropostaCommand command, CancellationToken cancellationToken)
    {
        var clienteResult = await inscricoesRepositorio.RecuperarCliente(command.Cliente);
        if (clienteResult.HasNoValue)
            return Result.Failure<Proposta>("Cliente inválido, favor cadastra-lo previamente.");

        if (await inscricoesRepositorio.ClientePossuiPropostaAberta(command.Cliente))
            return Result.Failure<Proposta>("Cliente ja possui uma proposta aberta.");

        if (await inscricoesRepositorio.PossuiRestricaoCpf(command.Cliente))
            return Result.Failure<Proposta>("Cliente esta com o CPF bloqueado.");

        var agenteResult = await inscricoesRepositorio.RecuperarAgente(command.AgenteId, cancellationToken);
        if (!agenteResult.Value.IsAtivo)
            return Result.Failure<Proposta>("Agente inativo, não foi possivel seguir com a proposta.");

        var conveniadaResult = await inscricoesRepositorio.RecuperarConveniada(command.Conveniada, cancellationToken);
        if (conveniadaResult.HasNoValue)
            return Result.Failure<Proposta>("Conveniada inválida");

        var regrasPorConveniada = await regraPorConveniadaRepository.ObterRegrasPorConveniadaAsync(conveniadaResult.Value.Id);

        var inscricaoResult = Proposta.Criar(
            clienteResult.Value,
            conveniadaResult.Value,
            command.AgenteId,
            command.QuantidadeParcelas,
            command.ValorSolicitado,
            regrasPorConveniada.Select(c => c.Regra));

        if (inscricaoResult.IsFailure)
            return Result.Failure<Proposta>(inscricaoResult.Error);

        var inscricao = inscricaoResult.Value;
        await inscricoesRepositorio.Adicionar(inscricao, cancellationToken);
        await inscricoesRepositorio.Save();

        return Result.Success(inscricao);
    }
}
