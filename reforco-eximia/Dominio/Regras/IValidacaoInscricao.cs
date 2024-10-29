using CSharpFunctionalExtensions;
using ReforcoEximia.HttpService.Dominio.Inscricoes;

namespace ReforcoEximia.HttpService.Dominio.Regras;

public interface IValidacaoInscricao
{
    Result Validar(Cliente cliente, Conveniada conveniada, int parcelas, decimal valorSolicitado);
}