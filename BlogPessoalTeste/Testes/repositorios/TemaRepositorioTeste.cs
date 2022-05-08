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
    public class TemaRepositorioTeste
    {
        private BlogPessoalContexto _contexto;
        private ITema _repositorio;
     
        [TestMethod]
        public void CriarQuatroTemasNoBancoRetornaQuatroTemas2()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            // GIVEN - Dado que registro 4 temas no banco
            _repositorio.NovoTema(new NovoTemaDTO("C#"));
            _repositorio.NovoTema(new NovoTemaDTO("Java"));
            _repositorio.NovoTema(new NovoTemaDTO("Python"));
            _repositorio.NovoTema(new NovoTemaDTO("JavaScript"));

            // THEN - Entao deve retornar 4 temas
            Assert.AreEqual(4, _repositorio.PegarTodosTemas().Count);
        }
        [TestMethod]
        public void PegarTemaPeloIdRetornaTema1()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro C# no banco
            _repositorio.NovoTema(new NovoTemaDTO("C#"));

            //WHEN - Quando pesquiso pelo id 1
            var tema = _repositorio.PegarTemaPeloId(1);

            //THEN - Entao deve retornar 1 tema
            Assert.AreEqual("C#", tema.Descricao);
        }

        [TestMethod]
        public void PegaTemaPelaDescricaoRetornaDoisTemas()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro Java no Banco
            _repositorio.NovoTema(new NovoTemaDTO("Java"));

            //AND - E que registro JavaScript no banco
            _repositorio.NovoTema(new NovoTemaDTO("JavaScript"));

            //WHEN - Quando pesquiso pela descricao Java
            var temas = _repositorio.PegarTemaPelaDescricao("Java");

            //THEN - Entao deve retornar 2 temas
            Assert.AreEqual(2, temas.Count);
        }
        [TestMethod]
        public void AlterarTemaPythonRetornaTemaCobol()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro Python no banco
            _repositorio.NovoTema(new NovoTemaDTO("Python"));

            //WHEN - Quando passo o Id 1 e o tema COBOL
            _repositorio.AtualizarTema(new AtualizarTemaDTO(1, "COBOL"));

            //THEN - Entao deve retornar o tema COBOL
            Assert.AreEqual("COBOL",
            _repositorio.PegarTemaPeloId(1).Descricao);
        }
        [TestMethod]
        public void DeletarTemasRetornaNulo()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal5")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new TemaRepositorio(_contexto);

            //GIVEN - Dado que registro 1 tema no banco
            _repositorio.NovoTema(new NovoTemaDTO("C#"));

            //WHEN - Quando passo o Id 1
            _repositorio.DeletarTema(1);

            //THEN - Entao deve retornar nulo
            Assert.IsNull(_repositorio.PegarTemaPeloId(1));
        }
    }
}
