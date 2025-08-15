using MyFlix.Dados.Entidades;

namespace MyFlix.Serve.Interfaces
{
    public interface IFilmesServe
    {
        Task DeletarAsync(int id);
        Task<Filmes> AtualizarAsync(Filmes filmes);
        Task CadastrarAsync(Filmes filmes);
        Task<IList<Filmes>> ObterTodosAsync();
        Task<Filmes> ObterPorIdAsync(int id);
        Task<bool> ValidarSeExisteFilmesAsync();
    }
}
