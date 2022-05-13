using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositorios.implementacoes
{
    public class TemaRepositorio : ITema
    {
        #region Atributos

        private readonly BlogPessoalContexto _contexto;

        #endregion Atributos

        #region Construtores

        public TemaRepositorio(BlogPessoalContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion Construtores

        #region Metodos

        public async Task AtualizarTemaAsync(AtualizarTemaDTO tema)
        {
            var temaExistente = await PegarTemaPeloIdAsync(tema.Id);
            temaExistente.Descricao = tema.Descricao;
            _contexto.Temas.Update(temaExistente);
            await _contexto.SaveChangesAsync();
        }

        public async Task DeletarTemaAsync(int id)
        {
            _contexto.Temas.Remove(await PegarTemaPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        public async Task NovoTemaAsync(NovoTemaDTO tema)
        {
            await _contexto.Temas.AddAsync(new TemaModelo
            {
                Descricao = tema.Descricao
            });
            await _contexto.SaveChangesAsync();
        }

        public async Task<TemaModelo> PegarTemaPeloIdAsync(int id)
        {
            return await _contexto.Temas.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TemaModelo>> PegarTemaPelaDescricaoAsync(string descricao)
        {
            return await _contexto.Temas
                .Where(t => t.Descricao.Contains(descricao))
                .ToListAsync();
        }

        public async Task<List<TemaModelo>> PegarTodosTemasAsync()
        {
            return await _contexto.Temas
                .ToListAsync();
        }
        #endregion Metodos
    }
}