using CSharpFunctionalExtensions;
using ReforcoEximia.HttpService.Dominio.Inscricoes;

namespace ReforcoEximia.HttpService.Dominio.Regras;

public class ValidacaoRestricoesConveniada : IValidacaoInscricao
{
    public Result Validar(Cliente cliente, Conveniada conveniada, int parcelas, decimal valorSolicitado)
    {
        if (conveniada.Nome == "XPTO" && cliente.Endereco == "RS" && valorSolicitado >= 80_0000m)
            return Result.Failure("Valor acima do limite para esse convênio.");
        return Result.Success();
    }
}