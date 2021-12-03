using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;

namespace CasaDoCodigo
{
  public  class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
  {

    private readonly IHttpContextAccessor contextAccessor;
    public PedidoRepository(ApplicationContext contexto, IHttpContextAccessor contextAccessor) : base(contexto)
    {
      this.contextAccessor = contextAccessor;
    }

    private int? ObterPedidoId() {
      return contextAccessor.HttpContext.Session.GetInt32("pedidoId");
    }

    private void DefinirPedidoId(int pedidoId) {
      contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
    }
  }
}
