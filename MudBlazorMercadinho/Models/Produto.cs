namespace MudBlazorMercadinho.Models;

public class Produto
{
    public Produto() { }

    public Produto(int id, string nome, decimal preco)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
    }

    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
}
