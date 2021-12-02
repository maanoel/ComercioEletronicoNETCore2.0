using CasaDoCodigo.Models;
using System.Collections.Generic;

namespace CasaDoCodigo
{
  public interface IProdutoRepository
  {
    void SaveProdutos(List<Livro> ros);

    IList<Produto> ObterProdutos();
  }
}