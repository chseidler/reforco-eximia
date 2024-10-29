using CSharpFunctionalExtensions;

namespace ReforcoEximia.HttpService.Dominio.Inscricoes;

public class Agente : Entity<Guid>
{
    public bool IsAtivo { get; }
}
