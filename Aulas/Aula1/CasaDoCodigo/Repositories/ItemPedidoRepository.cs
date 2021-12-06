using CasaDoCodigo.Models;
using System.Linq;

namespace CasaDoCodigo
{
  public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
  {
    public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
    {
    }

    public ItemPedido ObterItemPedido(int itemPedidoId)
    {
      return dbSet
          .Where(item => item.Id == itemPedidoId)
          .FirstOrDefault();
    }
  }
}
