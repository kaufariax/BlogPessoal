using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controladores
{
    [ApiController]
    [Route("api/Postagens")]
    [Produces("application/json")]
    public class PostagemControlador : ControllerBase
    {
        #region Atributos

        private readonly IPostagem _repositorio;

        #endregion

        #region Construtores

        public PostagemControlador(IPostagem repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion

        #region Metodos

        [HttpGet("id/{idPostagem}")]
        [Authorize]
        public IActionResult PegarPostagemPeloId([FromRoute] int idPostagem)
        {
            var postagem = _repositorio.PegarPostagemPeloId(idPostagem);

            if (postagem == null) return NotFound();

            return Ok(postagem);
        }

        [HttpGet]
        [Authorize]
        public IActionResult PegarTodasPostagens()
        {
            var lista = _repositorio.PegarTodasPostagens();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }

        [HttpGet("pesquisa")]
        [Authorize]
        public IActionResult PegarPostagensPorPesquisa(
            [FromQuery]string titulo,
            [FromQuery]string descricaoTema,
            [FromQuery]string nomeCriador)
        {
            var postagens = _repositorio.PegarPostagensPorPesquisa(titulo, descricaoTema, nomeCriador);

            if (postagens.Count < 1) return NoContent();

            return Ok(postagens);
        }

        [HttpPost]
        [Authorize]
        public IActionResult NovaPostagem([FromBody] NovaPostagemDTO postagem)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repositorio.NovaPostagem(postagem);
            return Created($"api/Postagens", postagem);
        }

        [HttpPut]
        [Authorize]
        public IActionResult AtualizarPostagem([FromBody] AtualizarPostagemDTO postagem)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repositorio.AtualizarPostagem(postagem);
            return Ok(postagem);
        }

        [HttpDelete("deletar/{idPostagem}")]
        [Authorize]
        public IActionResult DeletarPostagem([FromRoute] int idPostagem)
        {
            _repositorio.DeletarPostagem(idPostagem);
            return NoContent();
        }

        #endregion
    }
}
