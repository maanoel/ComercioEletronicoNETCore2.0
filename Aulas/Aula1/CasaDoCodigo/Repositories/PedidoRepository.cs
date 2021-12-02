using CasaDoCodigo.Models;

namespace CasaDoCodigo
{
  public  class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
  {
    public PedidoRepository(ApplicationContext contexto) : base(contexto)
    {
    }
  }
}
