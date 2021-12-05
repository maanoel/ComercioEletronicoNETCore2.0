using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Controllers
{
  public class PedidoController: Controller
  {
    private readonly IProdutoRepository produtoRepository;
    private readonly IPedidoRepository pedidoRepository;

    public PedidoController(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository)
    {
      this.pedidoRepository = pedidoRepository;
      this.produtoRepository = produtoRepository;
    }

    public IActionResult Carrossel() 
    {
      return View(this.produtoRepository.ObterProdutos());
    } 

    public IActionResult Carrinho()
    {
      Pedido pedido = pedidoRepository.ObterPedido();
      return View(pedido.Itens);
    }

    public IActionResult Cadastro()
    {
      return View();
    }
    public IActionResult Resumo()
    {
      return View();
    }
  }
}
