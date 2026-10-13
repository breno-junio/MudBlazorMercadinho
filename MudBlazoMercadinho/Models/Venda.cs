namespace MudBlazoMercadinho.Models;

public class Venda
{
    public Venda() { }
    public Venda(int id, DateTime dataVenda, List<ItemVenda> itens)
    {
        Id = id;
        DataVenda = dataVenda;
        Itens = itens;
    }
    public int Id { get; set; }
    public DateTime DataVenda { get; set; }
    public List<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
    public decimal ValorTotal => Itens.Sum(i => i.Subtotal);
}
