using CasaDoCodigo.Models;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo
{
  public class ProdutoRepository : IProdutoRepository
  {
    private readonly ApplicationContext contexto;

    public ProdutoRepository(ApplicationContext contexto)
    {
      this.contexto = contexto;
    }

    public IList<Produto> ObterProdutos()
    {
      return contexto.Set<Produto>().ToList();
    }

    public void SaveProdutos(List<Livro> livros)
    {
      foreach(var livro in livros)
      {
        contexto.Set<Produto>().Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
      }
    }

  }
}
