using CasaDoCodigo.Models;

namespace CasaDoCodigo
{

    public interface ICadastroRepository {

    void Update(int cadastroId, Cadastro novoCadastro);
    }
}
