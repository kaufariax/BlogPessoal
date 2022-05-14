using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using System.Threading.Tasks;

namespace BlogPessoal.src.servicos
{
    /// <summary>
    /// <para>Resumo: Interface Responsavel por representar ações de autenticação</para>
    /// <para>Criado por: Kauane Farias</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(NovoUsuarioDTO usuario);
        string GerarToken(UsuarioModelo usuario);
        Task<AutorizacaoDTO> PegarAutorizacaoAsync(AutenticarDTO autenticacao);
    }
}
