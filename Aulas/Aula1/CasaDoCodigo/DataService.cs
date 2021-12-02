using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
  public partial class DataService : IDataService
  {
    private readonly ApplicationContext contexto;

    public IProdutoRepository ProdutoRepositorio { get; }

    public DataService(ApplicationContext contexto, IProdutoRepository produtoRepositorio)
    {
      this.contexto = contexto;
      ProdutoRepositorio = produtoRepositorio;
    }

    public void InicializaDB()
    {
      contexto.Database.Migrate();

      List<Livro> livros = GetLivros();

      ProdutoRepositorio.SaveProdutos(livros);

      contexto.SaveChanges();
    }

    private static List<Livro> GetLivros()
    {
      var json = File.ReadAllText("livros.json");
      var livros = JsonConvert.DeserializeObject<List<Livro>>(json);
      return livros;
    }
  }
}
