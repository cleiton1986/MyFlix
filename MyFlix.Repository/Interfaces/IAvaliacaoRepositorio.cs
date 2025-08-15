using MyFlix.Dados.Entidades;

namespace MyFlix.Repository.Interfaces
{
    public interface IAvaliacaoRepositorio
    {
        Task<IEnumerable<Avaliacao>> ObterTodosAsync();
        Task<Avaliacao> ObterPorIdAsync(int id);
    }
}
