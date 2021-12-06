using CasaDoCodigo.Models;

namespace CasaDoCodigo
{
  public interface IPedidoRepository
  {
    Pedido ObterPedido();
    void AdicionarItem(string codigo);

     UpdateQuantidadeResponse UpdateQuantidade(ItemPedido itemPedido)

  }
}
