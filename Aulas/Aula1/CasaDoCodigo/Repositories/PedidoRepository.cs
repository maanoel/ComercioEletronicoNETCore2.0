using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CasaDoCodigo
{
  public  class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
  {
    private readonly IHttpContextAccessor contextAccessor;
    private readonly IItemPedidoRepository itemPedidoRepository;

    public PedidoRepository(ApplicationContext contexto, IHttpContextAccessor contextAccessor, IItemPedidoRepository itemPedidoRepository) : base(contexto)
    {
      this.contextAccessor = contextAccessor;
      this.itemPedidoRepository = itemPedidoRepository;
    }

    public Pedido ObterPedido()
    {
      var pedidoId = ObterPedidoId();
      var pedido = dbSet
      .Include(p=> p.Itens)
      .ThenInclude(i=>i.Produto)
      .Where(p => p.Id == pedidoId)
      .SingleOrDefault();

      if(pedido == null) {
        pedido = new Pedido();
        dbSet.Add(pedido);
        contexto.SaveChanges();
        DefinirPedidoId(pedido.Id);
      }

      return pedido;
    }



    private int? ObterPedidoId()
    {
      return contextAccessor.HttpContext.Session.GetInt32("pedidoId");
    }

    private void DefinirPedidoId(int pedidoId)
    {
      contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
    }

    public void AdicionarItem(string codigo)
    {
      var produto = contexto.Set<Produto>().Where(p => p.Codigo == codigo).FirstOrDefault();

      if(produto == null)
      {
        throw new ArgumentException("Produto não encontrado");
      }

      var pedido = ObterPedido();

      var itemPedido = contexto.Set<ItemPedido>()
        .Where(i => i.Produto.Codigo == codigo && i.Pedido.Id == pedido.Id)
        .SingleOrDefault();

      if(itemPedido == null) {
        itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
        contexto.Set<ItemPedido>().Add(itemPedido);
        contexto.SaveChanges();
      }

    }

    public UpdateQuantidadeResponse UpdateQuantidade(ItemPedido itemPedido)
    {
      var itemPedidoDb = itemPedidoRepository.ObterItemPedido(itemPedido.Id);

      if(itemPedidoDb != null)
      {
        itemPedidoDb.AtualizaQuantidade(itemPedido.Quantidade);

        if(itemPedido.Quantidade == 0) {
          itemPedidoRepository.RemoverItemPedido(itemPedido.Id);
        }

        contexto.SaveChanges();

        var carrinhoViewModel = new CarrinhoViewModel(ObterPedido().Itens);

        return new UpdateQuantidadeResponse(itemPedidoDb, carrinhoViewModel);
      }

      throw new ArgumentException("ItemPedido não encontrado.");
    }
  }
}
