using MyFlix.AppServer.ViewModel;

namespace MyFlix.AppServer.Interfaces
{
    public interface IAvaliacaoAppServe
    {
        Task<IList<AvaliacaoViewModel>> ObterTodosAsync();
        Task<AvaliacaoViewModel> ObterPorIdAsync(int id);
    }
}
