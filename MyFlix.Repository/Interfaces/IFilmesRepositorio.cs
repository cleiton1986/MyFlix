using MyFlix.Dados.Entidades;

namespace MyFlix.Repository.Interfaces
{
    public interface IFilmesRepositorio
    {
        Task DeletarAsync(Filmes filmes);
        Task AtualizarAsync(Filmes filmes);
        Task CadastrarAsync(Filmes filmes);
        Task<IEnumerable<Filmes>> ObterTodosAsync();
        Task<Filmes> ObterPorIdAsync(int id);
        Task<bool> ValidarSeExisteFilmesAsync();
    }
}
