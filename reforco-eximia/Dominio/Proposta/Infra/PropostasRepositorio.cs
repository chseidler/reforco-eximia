using CSharpFunctionalExtensions;
using Dapper;
using ReforcoEximia.HttpService.Comum;
using Microsoft.EntityFrameworkCore;

namespace ReforcoEximia.HttpService.Dominio.Inscricoes.Infra;

public sealed class PropostasRepositorio(PropostasDbContext dbContext) : IService<PropostasRepositorio>
{
    public async Task<bool> ClientePossuiPropostaAberta(string cpf)
    {
        var result = await dbContext.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<string>("SELECT cpf_cliente FROM Propostas.Propostas WHERE cpf_cliente = @cpf and status = 1",
                new { cpf });
        return result == cpf;
    }

    public async Task<bool> PossuiRestricaoCpf(string cpf)
    {
        var result = await dbContext.Database.GetDbConnection()
            .QueryFirstOrDefaultAsync<string>("SELECT cpf_cliente FROM Propostas.Clientes WHERE cpf = @cpf and cpf_bloqueado = 1",
            new { cpf });

        return result == cpf;
    }

    public async Task<Maybe<Cliente>> RecuperarCliente(string cpf)
    {
        return (await dbContext.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf)) ?? Maybe<Cliente>.None;
    }

    public async Task<Maybe<Agente>> RecuperarAgente(Guid id, CancellationToken cancellationToken)
    {
        var agente = await dbContext.Agentes.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return agente ?? Maybe<Agente>.None;
    }

    public async Task<Maybe<Conveniada>> RecuperarConveniada(string conveniada, CancellationToken cancellationToken)
    {
        var agente = await dbContext.Conveniadas.FirstOrDefaultAsync(c => c.Nome == conveniada, cancellationToken);
        return agente ?? Maybe<Conveniada>.None;
    }

    public async Task Adicionar(Proposta inscricao, CancellationToken cancellationToken)
    {
        await dbContext.Propostas.AddAsync(inscricao, cancellationToken);
    }

    public Task Save()
    {
        return dbContext.SaveChangesAsync();
    }
}