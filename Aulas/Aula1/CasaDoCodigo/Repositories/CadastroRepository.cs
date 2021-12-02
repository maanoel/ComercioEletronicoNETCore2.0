using CasaDoCodigo.Models;

namespace CasaDoCodigo
{
  public  class CadastroRepository : BaseRepository<Cadastro>, ICadastroRepository
  {
    public CadastroRepository(ApplicationContext contexto) : base(contexto)
    {
    }
  }
}
