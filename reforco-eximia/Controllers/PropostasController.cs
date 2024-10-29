using ReforcoEximia.HttpService.Dominio.Inscricoes.Aplicacao;
using Microsoft.AspNetCore.Mvc;

namespace reforco_eximia.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/{controller}")]
[ApiVersion("1.0")]
public sealed class PropostasController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RealizarInscricao(
        [FromBody] NovaInscricaoModel input,
        [FromServices] GerarPropostaHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new GerarPropostaCommand(
            Cliente: input.CpfCliente,
            ValorSolicitado: input.ValorSolicitado,
            Conveniada: input.Conveniada,
            QuantidadeParcelas: input.QuantidadeParcelas,
            AgenteId: input.AgenteId
        );

        var result = await handler.Handle(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}

public record NovaInscricaoModel(
    string CpfCliente,
    decimal ValorSolicitado,
    string Conveniada,
    int QuantidadeParcelas,
    Guid AgenteId
);