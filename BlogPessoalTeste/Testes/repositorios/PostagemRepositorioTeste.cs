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
    public class PostagemRepositorioTeste
    {
        private BlogPessoalContexto _contexto;
        private IUsuario _repositorioU;
        private ITema _repositorioT;
        private IPostagem _repositorioP;

        [TestMethod]
        public void CriaTresPostagensNoSistemaRetornaTres()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                 .UseInMemoryDatabase(databaseName: "db_blogpessoal20")
                 .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorioU = new UsuarioRepositorio(_contexto);
            _repositorioT = new TemaRepositorio(_contexto);
            _repositorioP = new PostagemRepositorio(_contexto);

            // GIVEN - Dado que registro 2 usuarios
            _repositorioU.NovoUsuario(
                new NovoUsuarioDTO
                ("Kauane Farias", "kau@email.com", "134652", "URLDAFOTO")
            );

            _repositorioU.NovoUsuario(
                new NovoUsuarioDTO
                ("Guilherme Reluz", "julio@email.com", "134652", "URLDAFOTO")
            );

            // AND - E que registro 2 temas
            _repositorioT.NovoTema(new NovoTemaDTO("C#"));
            _repositorioT.NovoTema(new NovoTemaDTO("Java"));

            // WHEN - Quando registro 3 postagens no sistema
            _repositorioP.NovaPostagem(
            new NovaPostagemDTO(
            "C# é muito massa",
            "É uma linguagem muito utilizada no mundo",
            "URLDAFOTO",
            "kau@email.com",
            "C#"
            )
            );
            _repositorioP.NovaPostagem(
            new NovaPostagemDTO(
            "C# pode ser usado com Testes",
            "O teste unitário é importante para o desenvolvimento",
            "URLDAFOTO",
            "julio@email.com",
            "C#"
            )
            );
            _repositorioP.NovaPostagem(
            new NovaPostagemDTO(
            "Java é muito massa",
            "Java também é muito utilizada no mundo",
            "URLDAFOTO",
            "kau@email.com",
            "Java"
            )
            );

            // WHEN - Quando eu busco todas as postagens
            // THEN - Eu tenho 3 postagens
            Assert.AreEqual(3, _repositorioP.PegarTodasPostagens().Count());
        }
        [TestMethod]
        public void AtualizarPostagemRetornaPostagemAtualizada()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal21")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorioU = new UsuarioRepositorio(_contexto);
            _repositorioT = new TemaRepositorio(_contexto);
            _repositorioP = new PostagemRepositorio(_contexto);

            // GIVEN - Dado que registro 1 usuario
            _repositorioU.NovoUsuario(
                new NovoUsuarioDTO
                ("Kauane Farias", "kau@email.com", "134652", "URLDAFOTO")
            );

            // AND - E que registro 1 tema
            _repositorioT.NovoTema(new NovoTemaDTO("COBOL"));
            _repositorioT.NovoTema(new NovoTemaDTO("C#"));

            // AND - E que registro 1 postagem
            _repositorioP.NovaPostagem(
                new NovaPostagemDTO(
                    "COBOL é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "kau@email.com",
                    "COBOL"
                )
            );

            // WHEN - Quando atualizo postagem de id 1
            _repositorioP.AtualizarPostagem(
                new AtualizarPostagemDTO(
                    1,
                    "C# é muito massa",
                    "C# é muito utilizada no mundo",
                    "URLDAFOTOATUALIZADA",
                    "C#"
                )
            );

            // THEN - Eu tenho a postagem atualizada
            Assert.AreEqual(
                "C# é muito massa",
                _repositorioP.PegarPostagemPeloId(1).Titulo
            );
            Assert.AreEqual(
                "C# é muito utilizada no mundo",
                _repositorioP.PegarPostagemPeloId(1).Descricao
            );
            Assert.AreEqual(
                "URLDAFOTOATUALIZADA",
                _repositorioP.PegarPostagemPeloId(1).Foto
            );
            Assert.AreEqual(
                "C#",
                _repositorioP.PegarPostagemPeloId(1).Tema.Descricao
            );
        }
        [TestMethod]
        public void PegarPostagensPorPesquisaRetornarCustomizada()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal22")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
            _repositorioU = new UsuarioRepositorio(_contexto);
            _repositorioT = new TemaRepositorio(_contexto);
            _repositorioP = new PostagemRepositorio(_contexto);

            // GIVEN - Dado que registro 2 usuarios
            _repositorioU.NovoUsuario(
                new NovoUsuarioDTO
                ("Kauane Farias", "kau@email.com", "134652", "URLDAFOTO")
            );

            _repositorioU.NovoUsuario(
                new NovoUsuarioDTO
                ("Gustavo Gama", "catel@email.com", "134652", "URLDAFOTO")
            );

            // AND - E que registro 2 temas
            _repositorioT.NovoTema(new NovoTemaDTO("C#"));
            _repositorioT.NovoTema(new NovoTemaDTO("Java"));

            // WHEN - Quando registro 3 postagens
            _repositorioP.NovaPostagem(
                new NovaPostagemDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "kau@email.com",
                    "C#"
                )
            );
            _repositorioP.NovaPostagem(
                new NovaPostagemDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catel@email.com",
                    "C#"
                )
            );
            _repositorioP.NovaPostagem(
                new NovaPostagemDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "kau@email.com",
                    "Java"
                )
            );

            // WHEN - Quando eu busco as postagens
            // THEN - Eu tenho as postagens que correspondem aos criterios
            Assert.AreEqual(
                2,
                _repositorioP.PegarPostagensPorPesquisa("massa", null, null).Count
            );
            Assert.AreEqual(
                2,
                _repositorioP.PegarPostagensPorPesquisa(null, "C#", null).Count
            );
            Assert.AreEqual(
                2,
                _repositorioP.PegarPostagensPorPesquisa(null, null, "Ka").Count
            );
        }
    }
}