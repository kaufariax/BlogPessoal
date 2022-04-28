using Microsoft.EntityFrameworkCore;
using BlogPessoal.src.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogPessoal.src.modelos;
using System.Linq;

namespace BlogPessoalTeste.Testes.data
{
    [TestClass]
    public class BlogPessoalContextoTeste
    {
        private BlogPessoalContexto _contexto;

        [TestInitialize]
        public void inicio()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
        }

         [TestMethod]
        public void InserirNovoUsuarioNoBancoDeDadosRetornarUsuario()
        {
            UsuarioModelo usuario = new UsuarioModelo();

            usuario.Nome = "Kauane Boaz";
            usuario.Email = "kauane@email.com";
            usuario.Senha = "134652";
            usuario.Foto = "AquiEstaOLinkDaFoto";

            _contexto.Usuarios.Add(usuario); // Adicionando usuario

            _contexto.SaveChanges(); // Commita criação

            Assert.IsNotNull(_contexto.Usuarios.FirstOrDefault(u => u.Email == "kauane@email.com"));
        }
    }
}
