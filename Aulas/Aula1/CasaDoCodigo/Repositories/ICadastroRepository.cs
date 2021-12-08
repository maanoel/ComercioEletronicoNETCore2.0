using CasaDoCodigo.Models;

namespace CasaDoCodigo
{

    public interface ICadastroRepository {

    CadastroRepository Update(int cadastroId, Cadastro novoCadastro);
    }
}
