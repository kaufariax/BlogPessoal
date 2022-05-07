using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositorios.implementacoes
{
    public class UsuarioRepositorio : IUsuario
    {
        #region Atributos

        private readonly BlogPessoalContexto _contexto;

        #endregion Atributos

        #region Construtores

        public UsuarioRepositorio(BlogPessoalContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion Construtores

        #region Metodos

        public void AtualizarUsuario(AtualizarUsuarioDTO usuario)
        {
            var usuarioExistente = PegarUsuarioPeloId(usuario.Id);
            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Senha = usuario.Senha;
            usuarioExistente.Foto = usuario.Foto;
            _contexto.Usuarios.Update(usuarioExistente);
            _contexto.SaveChanges();
        }

        public void DeletarUsuario(int id)
        {
            _contexto.Usuarios.Remove(PegarUsuarioPeloId(id));
            _contexto.SaveChanges();
        }

        public void NovoUsuario(NovoUsuarioDTO usuario)
        {
            _contexto.Usuarios.Add(new UsuarioModelo
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Foto = usuario.Foto
            });
            _contexto.SaveChanges();
        }

        public UsuarioModelo PegarUsuarioPeloEmail(string email)
        {
          return _contexto.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public UsuarioModelo PegarUsuarioPeloId(int id)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public List<UsuarioModelo> PegarUsuariosPeloNome(string nome)
        {
            return _contexto.Usuarios
                .Where(u => u.Nome.Contains(nome))
                .ToList();
        }
        #endregion Metodos
    }
}
