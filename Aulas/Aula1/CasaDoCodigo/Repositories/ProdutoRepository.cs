using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo
{
  public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
  {
    public ProdutoRepository(ApplicationContext contexto) : base(contexto)
    {
    }

    public IList<Produto> ObterProdutos()
    {
      return contexto.Set<Produto>().ToList();
    }

    public void SaveProdutos(List<Livro> livros)
    {
      foreach(var livro in livros)
      {
        if(!dbSet.Where(p => p.Codigo == livro.Codigo).Any()) 
        { 
          contexto.Set<Produto>().Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
        }
      }

      contexto.SaveChanges();
    }
  }
}
