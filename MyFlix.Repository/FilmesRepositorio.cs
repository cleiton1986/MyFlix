using Microsoft.EntityFrameworkCore;
using MyFlix.Dados.Cofiguracao;
using MyFlix.Dados.Entidades;
using MyFlix.Repository.Interfaces;

namespace MyFlix.Repository
{
    public class FilmesRepositorio : IFilmesRepositorio
    {
        private readonly Context _context;

        public FilmesRepositorio(Context context)
        {
            _context = context;
        }

        public async Task AtualizarAsync(Filmes filmes)
        {
            _context.Filmes.Update(filmes);
           await _context.SaveChangesAsync();
        }

        public async Task CadastrarAsync(Filmes filmes)
        {
            await _context.Filmes.AddAsync(filmes);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(Filmes filmes)
        {
             _context.Filmes.Remove(filmes);
            await _context.SaveChangesAsync();
        }

        public async Task<Filmes> ObterPorIdAsync(int id)
        {
            var filme = await _context.Filmes
                                      .Include(f => f.Avaliacao)
                                      .FirstOrDefaultAsync(f => f.Id == id);
            return filme;
        }

        public async Task<IEnumerable<Filmes>> ObterTodosAsync()
        {
            return await _context.Filmes
                                 .Include(f => f.Avaliacao)
                                 .ToListAsync();
        }
        public async Task<bool> ValidarSeExisteFilmesAsync()
        {
            return await _context.Filmes.AnyAsync();
        }
    }
}
