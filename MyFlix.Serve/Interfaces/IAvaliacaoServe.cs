using MyFlix.Dados.Entidades;

namespace MyFlix.Serve.Interfaces
{
    public interface IAvaliacaoServe
    {
        Task<IList<Avaliacao>> ObterTodosAsync();
        Task<Avaliacao> ObterPorIdAsync(int id);
    }
}
