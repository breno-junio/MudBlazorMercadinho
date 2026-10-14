using MudBlazorMercadinho.Models;

namespace MudBlazorMercadinho.Services;

public class MercadinhoService
{
    // Simulação do Banco em Memória
    private readonly List<Produto> _produtos = new()
    {
        new Produto(1, "Arroz 5kg", 28.90m),
        new Produto(2, "Feijão Carioca 1kg", 7.50m),
        new Produto(3, "Óleo de Soja 900ml", 6.80m),
        new Produto(4, "Leite Integral 1L", 4.99m),
        new Produto(5, "Café Torrado 500g", 16.50m)
    };

    private readonly Dictionary<int, int> _estoque = new()
    {
        { 1, 50 },
        { 2, 30 },
        { 3, 100 },
        { 4, 12 },
        { 5, 0 }
    };

    private readonly List<Venda> _vendas = new();
    private int _proximoVendaId = 1;

    public MercadinhoService() { }



    #region Operações de Produtos e Estoque

    public Task<List<ProdutoComEstoque>> ObterProdutosAgrupadosAsync()
    {
        var resultado = _produtos.Select(p => new ProdutoComEstoque
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            QuantidadeEmEstoque = _estoque.GetValueOrDefault(p.Id, 0)
        }).ToList();
        return Task.FromResult(resultado);
    }

    public Task<bool> AdicionarProdutoAsync(Produto produto)
    {
        if (_produtos.Any(p => p.Id == produto.Id || produto.Id <= 0))
            return Task.FromResult(false);

        _produtos.Add(produto);
        _estoque[produto.Id] = 0;
        return Task.FromResult(true);
    }

    public Task<bool> DeletarProdutoAsync(int id)
    {
        var prod = _produtos.FirstOrDefault(p => p.Id == id);
        if (prod == null) return Task.FromResult(false);

        _produtos.Remove(prod);
        _estoque.Remove(id);
        return Task.FromResult(true);
    }

    public Task AdicionarEstoqueAsync(int produtoId, int quantidade)
    {
        if (_estoque.ContainsKey(produtoId) && quantidade > 0)
        {
            _estoque[produtoId] += quantidade;
        }
        return Task.CompletedTask;
    }
    #endregion

    #region Operações de Vendas

    public Task<bool> RegistrarVendaAsync(List<(int produtoId, int quantidade)> itens)
    {
        if (itens == null || !itens.Any()) return Task.FromResult(false);

        // Validar estoque disponível antes da baixa
        foreach (var item in itens)
        {
            var qtdDisponivel = _estoque.GetValueOrDefault(item.produtoId, 0);
            if (qtdDisponivel < item.quantidade) return Task.FromResult(false);
        }

        // Processa a baixa no estoque e cria os itens da venda
        var itensVenda = new List<ItemVenda>();

        foreach (var item in itens)
        {
            var prod = _produtos.First(p => p.Id == item.produtoId);
            _estoque[item.produtoId] -= item.quantidade;

            itensVenda.Add(new ItemVenda(prod.Id, prod.Nome, prod.Preco, item.quantidade));
        }

        var novaVenda = new Venda(_proximoVendaId++, DateTime.Now, itensVenda);
        _vendas.Add(novaVenda);
        return Task.FromResult(true);
    }

    public async Task InicializarDadosAsync()
    {

        if (!_vendas.Any())
        {
            var itensTeste = new List<(int produtoId, int quantidade)>
            {
                (1, 2),
                (2, 1)
            };


            await RegistrarVendaAsync(itensTeste);
        }
    }



    public Task<List<Venda>> ObterVendasAsync() => Task.FromResult(_vendas);

    #endregion
}
