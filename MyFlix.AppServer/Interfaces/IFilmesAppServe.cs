using MyFlix.AppServer.ViewModel;

namespace MyFlix.AppServer.Interfaces
{
    public interface IFilmesAppServe
    {
        Task DeletarAsync(int id);
        Task<FilmesViewModel> AtualizarAsync(FilmesViewModel filmesView);
        Task CadastrarAsync(FilmesViewModel filmesView);
        Task<IList<FilmesViewModel>> ObterTodosAsync();
        Task<FilmesViewModel> ObterPorIdAsync(int id);
        Task<bool> ValidarSeExisteFilmesAsync();
    }
}
