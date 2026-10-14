namespace MudBlazorMercadinho.Models;

public class ProdutoComEstoque
{
    public ProdutoComEstoque() { }
    public ProdutoComEstoque(int id, string nome, decimal preco, int quantidadeEmEstoque)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
        QuantidadeEmEstoque = quantidadeEmEstoque;
    }
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int QuantidadeEmEstoque { get; set; }
}
