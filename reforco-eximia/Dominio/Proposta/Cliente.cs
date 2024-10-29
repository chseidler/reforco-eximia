using CSharpFunctionalExtensions;

namespace ReforcoEximia.HttpService.Dominio.Inscricoes;

public sealed class Cliente : Entity<Guid>
{
    public string Cpf { get; }
    public decimal Rendimento { get; }
    public int Idade { get; }
    public string Endereco { get; }
    public string Telefone { get; }
    public string Email { get; }
}
