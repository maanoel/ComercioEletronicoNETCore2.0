using CasaDoCodigo.Models;
using System.Linq;

namespace CasaDoCodigo
{
  public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
  {
    public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
    {
    }

    public void UpdateQuantidade(ItemPedido itemPedido)
    {
      var itemPedidoDb = dbSet
          .Where(item => item.Id == itemPedido.Id)
          .FirstOrDefault();

      if(itemPedidoDb != null) {
        itemPedidoDb.AtualizaQuantidade(itemPedido.Quantidade);
        contexto.SaveChanges();
      } 
    }
  }
}
