using System.Linq;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using BlogPessoal.src.repositorios.implementacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Testes.repositorios
{
    [TestClass]
    public class UsuarioRepositorioTeste
    {
        private BlogPessoalContexto _contexto;
        private IUsuario _repositorio;
        [TestInitialize]
        public void ConfiguracaoInicial()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal")
            .Options;
            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new UsuarioRepositorio(_contexto);
        }
        [TestMethod]
        public void CriarQuatroUsuariosNoBancoRetornaQuatroUsuarios()
        {
            //GIVEN - Dado que registro 4 usuarios no banco
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Kauane Farias",
            "kauane@email.com",
            "134652",
            "URLFOTO"));
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Lucas Grillo",
            "cesar@email.com",
            "134652",
            "URLFOTO"));
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Guilherme Carvalho",
            "cavalcanti@email.com",
            "134652",
            "URLFOTO"));
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Murilo Peres",
            "catel@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _contexto.Usuarios.Count());
        }
        [TestMethod]
        public void PegarUsuarioPeloEmailRetornaNaoNulo()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Uriel Franca",
            "zimerer@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repositorio.PegarUsuarioPeloEmail("zimerer@email.com");
            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }
        [TestMethod]
        public void PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuario()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Karol Alves",
            "karol@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando pesquiso pelo id 6
            var user = _repositorio.PegarUsuarioPeloId(6);
            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Karol Alves
            Assert.AreEqual("Karol Alves", user.Nome);
        }
        [TestMethod]
        public void AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Clauber Santos",
            "boaz@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando atualizamos o usuario
            var antigo =
            _repositorio.PegarUsuarioPeloEmail("boaz@email.com");
            _repositorio.AtualizarUsuario(
            new AtualizarUsuarioDTO(
            7,
            "Clauber Boaz",
            "123456",
            "URLFOTONOVA"));
            //THEN - Então, quando validamos pesquisa deve retornar nome Clauber Boaz
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
