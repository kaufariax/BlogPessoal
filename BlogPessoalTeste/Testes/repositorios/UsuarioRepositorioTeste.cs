using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using BlogPessoal.src.repositorios.implementacoes;
using BlogPessoal.src.utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Testes.repositorios
{
    [TestClass]
    public class UsuarioRepositorioTeste
    {
        private BlogPessoalContexto _contexto;
        private IUsuario _repositorio;

        [TestMethod]
        public async Task CriarQuatroUsuariosNoBancoRetornaQuatroUsuariosAsync()
        {
            //Definindo o contexto
          var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
            .Options;

        _contexto = new BlogPessoalContexto(opt);
        _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro 4 usuarios no banco
            await _repositorio.NovoUsuarioAsync(
            new NovoUsuarioDTO(
            "Kauane Farias",
            "kauane@email.com",
            "134652",
            "URLFOTO",
            TipoUsuario.NORMAL)
            );
            await _repositorio.NovoUsuarioAsync(
            new NovoUsuarioDTO(
            "Lucas Grillo",
            "cesar@email.com",
            "134652",
            "URLFOTO",
            TipoUsuario.NORMAL)
            );
            await _repositorio.NovoUsuarioAsync(
            new NovoUsuarioDTO(
            "Guilherme Carvalho",
            "cavalcanti@email.com",
            "134652",
            "URLFOTO",
            TipoUsuario.NORMAL)
            );
            await _repositorio.NovoUsuarioAsync(
            new NovoUsuarioDTO(
            "Murilo Peres",
            "catel@email.com",
            "134652",
            "URLFOTO",
            TipoUsuario.NORMAL)
            );

            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _contexto.Usuarios.Count()
            );
        }
        [TestMethod]
        public async Task PegarUsuarioPeloEmailRetornaNaoNuloAsync()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro um usuario no banco
            await _repositorio.NovoUsuarioAsync(
            new NovoUsuarioDTO(
            "Uriel Franca",
            "zimerer@email.com",
            "134652",
            "URLFOTO",
            TipoUsuario.NORMAL));

            //WHEN - Quando pesquiso pelo email deste usuario
            var usuario = await _repositorio.PegarUsuarioPeloEmailAsync("zimerer@email.com");

            //THEN - Então obtenho um usuario
            Assert.IsNotNull(usuario);
        }
        [TestMethod]
        public async Task PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuarioAsync()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro um usuario no banco
            await _repositorio.NovoUsuarioAsync(
            new NovoUsuarioDTO(
            "Karol Alves",
            "karol@email.com",
            "134652",
            "URLFOTO",
            TipoUsuario.NORMAL)
            );

            //WHEN - Quando pesquiso pelo id 1
            var usuario = await _repositorio.PegarUsuarioPeloIdAsync(1);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(usuario);

            //THEN - Então, o elemento deve ser Karol Alves
            Assert.AreEqual("Karol Alves", usuario.Nome);
        }
        [TestMethod]
        public async Task AtualizarUsuarioRetornaUsuarioAtualizadoAsync()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new UsuarioRepositorio(_contexto);

            //GIVEN - Dado que registro um usuario no banco
            await _repositorio.NovoUsuarioAsync(
            new NovoUsuarioDTO(
            "Clauber Santos",
            "boaz@email.com",
            "134652",
            "URLFOTO",
            TipoUsuario.NORMAL)
            );

            //WHEN - Quando atualizamos o usuario
            await _repositorio.AtualizarUsuarioAsync( 
            new AtualizarUsuarioDTO(
            1,
            "Clauber Boaz",
            "123456",
            "URLFOTONOVA")
            );

            //THEN - Então, quando validamos pesquisa deve retornar nome Clauber Boaz
            var antigo = await _repositorio.PegarUsuarioPeloEmailAsync("boaz@email.com");

            Assert.AreEqual(
            "Clauber Boaz",
            _contexto.Usuarios.FirstOrDefault(u => u.Id == antigo.Id).Nome);

            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.AreEqual(
            "123456",
            _contexto.Usuarios.FirstOrDefault(u => u.Id == antigo.Id).Senha);
        }
    }
}
