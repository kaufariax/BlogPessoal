using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using BlogPessoal.src.repositorios.implementacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Testes.repositorios
{
    [TestClass]
    public class TemaRepositorioTeste
    {
        private BlogPessoalContexto _contexto;
        private ITema _repositorio;
     
        [TestMethod]
        public async Task CriarQuatroTemasNoBancoRetornaQuatroTemas2Async()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            // GIVEN - Dado que registro 4 temas no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("C#"));
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("Java"));
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("Python"));
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("JavaScript"));

            var temas = await _repositorio.PegarTodosTemasAsync();

            // THEN - Entao deve retornar 4 temas
            Assert.AreEqual(4, temas.Count);
        }
        [TestMethod]
        public async Task PegarTemaPeloIdRetornaTema1Async()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro C# no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("C#"));

            //WHEN - Quando pesquiso pelo id 1
            var tema = await _repositorio.PegarTemaPeloIdAsync(1);

            //THEN - Entao deve retornar 1 tema
            Assert.AreEqual("C#", tema.Descricao);
        }

        [TestMethod]
        public async Task PegaTemaPelaDescricaoRetornaDoisTemasAsync()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro Java no Banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("Java"));

            //AND - E que registro JavaScript no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("JavaScript"));

            //WHEN - Quando pesquiso pela descricao Java
            var temas = await _repositorio.PegarTemaPelaDescricaoAsync("Java");

            //THEN - Entao deve retornar 2 temas
            Assert.AreEqual(2, temas.Count);
        }
        [TestMethod]
        public async Task AlterarTemaPythonRetornaTemaCobolAsync()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro Python no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("Python"));

            //WHEN - Quando passo o Id 1 e o tema COBOL
            await _repositorio.AtualizarTemaAsync(new AtualizarTemaDTO(1, "COBOL"));

            var tema = await _repositorio.PegarTemaPeloIdAsync(1);

            //THEN - Entao deve retornar o tema COBOL
            Assert.AreEqual("COBOL", tema.Descricao);
        }
        [TestMethod]
        public async Task DeletarTemasRetornaNuloAsync()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal5")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro 1 tema no banco
            await _repositorio.NovoTemaAsync(new NovoTemaDTO("C#"));

            //WHEN - Quando passo o Id 1
            await _repositorio.DeletarTemaAsync(1);

            //THEN - Entao deve retornar nulo
            Assert.IsNull(await _repositorio.PegarTemaPeloIdAsync(1));
        }
    }
}
