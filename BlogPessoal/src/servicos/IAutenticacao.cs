using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using System.Threading.Tasks;

namespace BlogPessoal.src.servicos
{
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(NovoUsuarioDTO usuario);
        string GerarToken(UsuarioModelo usuario);
        Task<AutorizacaoDTO> PegarAutorizacaoAsync(AutenticarDTO autenticacao);
    }
}
