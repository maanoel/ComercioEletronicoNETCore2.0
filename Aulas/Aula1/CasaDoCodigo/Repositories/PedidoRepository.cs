using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace CasaDoCodigo
{
  public  class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
  {
    private readonly IHttpContextAccessor contextAccessor;
    public PedidoRepository(ApplicationContext contexto, IHttpContextAccessor contextAccessor) : base(contexto)
    {
      this.contextAccessor = contextAccessor;
    }

    public Pedido ObterPedido()
    {
      var pedidoId = ObterPedidoId();
      var pedido = dbSet
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

  }
}
