using CasaDoCodigo.Models;

namespace CasaDoCodigo
{
  public interface IItemPedidoRepository {

    ItemPedido ObterItemPedido(int itemPedidoId);

    void RemoverItemPedido(int itemPedidoId);


  }
}
