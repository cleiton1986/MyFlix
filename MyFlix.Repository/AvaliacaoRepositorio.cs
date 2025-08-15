using Microsoft.EntityFrameworkCore;
using MyFlix.Dados.Cofiguracao;
using MyFlix.Dados.Entidades;
using MyFlix.Repository.Interfaces;

namespace MyFlix.Repository
{
    public class AvaliacaoRepositorio: IAvaliacaoRepositorio
    {
        private readonly Context _context;

        public AvaliacaoRepositorio(Context context)
        {
            _context = context;
        }

        public async Task<Avaliacao> ObterPorIdAsync(int id)
        {
            var avaliacao = await _context.Avaliacao
                                          .FirstOrDefaultAsync(f => f.Id == id);
            return avaliacao;
        }

        public async Task<IEnumerable<Avaliacao>> ObterTodosAsync()
        {
            return await _context.Avaliacao
                                 .ToListAsync();
        }
    }
}
