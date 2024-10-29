namespace ReforcoEximia.HttpService.Dominio.Inscricoes.Aplicacao;

public record GerarPropostaCommand(
    string Cliente,
    decimal ValorSolicitado,
    string Conveniada,
    int QuantidadeParcelas,
    Guid AgenteId
);
