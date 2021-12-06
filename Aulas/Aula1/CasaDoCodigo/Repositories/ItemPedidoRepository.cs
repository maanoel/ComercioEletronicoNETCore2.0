using CasaDoCodigo.Models;

namespace CasaDoCodigo
{
  public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
  {
    public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
    {
    }

    public void UpdateQuantidade(ItemPedido itemPedido)
    {

    }
  }
}
