using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Controllers
{
  public class PedidoController : Controller
  {
    private readonly IProdutoRepository produtoRepository;
    private readonly IPedidoRepository pedidoRepository;
    private readonly IItemPedidoRepository itemPedidoRepository;

    public PedidoController(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository, IItemPedidoRepository itemPedidoRepository)
    {
      this.pedidoRepository = pedidoRepository;
      this.produtoRepository = produtoRepository;
      this.itemPedidoRepository = itemPedidoRepository;
    }

    public IActionResult Carrossel()
    {
      return View(this.produtoRepository.ObterProdutos());
    }

    public IActionResult Carrinho(string codigo)
    {
      if(!string.IsNullOrEmpty(codigo))
      {
        pedidoRepository.AdicionarItem(codigo);
      }

      Pedido pedido = pedidoRepository.ObterPedido();
      CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(pedido.Itens);
      return View(carrinhoViewModel);
    }

    public IActionResult Cadastro()
    {
      var pedido = pedidoRepository.ObterPedido();

      if(pedido == null)
        return RedirectToAction("Carrossel");

      return View(pedido.Cadastro);
    }

    [HttpPost]
    public IActionResult Resumo(Cadastro cadastro)
    {
      if(ModelState.IsValid)
      {
        Pedido pedido = pedidoRepository.UpdateCadastro(cadastro);
        return View(pedido);
      }

      return RedirectToAction("Cadastro");
    }

    [HttpPost]
    public UpdateQuantidadeResponse UpdateQuantidade([FromBody] ItemPedido itemPedido)
    {
      return pedidoRepository.UpdateQuantidade(itemPedido);
    }
  }
}
