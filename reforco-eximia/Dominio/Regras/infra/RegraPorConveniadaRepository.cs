using Microsoft.EntityFrameworkCore;

namespace ReforcoEximia.HttpService.Dominio.Regras.infra;

public class RegraPorConveniadaRepository(PropostasDbContext context)
{
    public async Task<IEnumerable<RegraPorConveniada>> ObterRegrasPorConveniadaAsync(Guid conveniadaId)
    {
        return await context.RegrasPorTurma
            .Where(r => r.Id == conveniadaId)
            .ToListAsync();
    }
}
