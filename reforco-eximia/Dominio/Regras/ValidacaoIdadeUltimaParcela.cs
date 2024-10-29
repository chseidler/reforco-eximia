using CSharpFunctionalExtensions;
using ReforcoEximia.HttpService.Dominio.Inscricoes;

namespace ReforcoEximia.HttpService.Dominio.Regras;

public class ValidacaoIdadeUltimaParcela : IValidacaoInscricao
{
    public Result Validar(Cliente cliente, Conveniada convenianada, int parcelas, decimal valorSolicitado)
    {
        int idadeLimite = 80;
        int mesesRestantes = (idadeLimite - cliente.Idade) * 12;
        if (parcelas > mesesRestantes)
            return Result.Failure("Cliente acima do limite de idade.");
        return Result.Success();
    }
}